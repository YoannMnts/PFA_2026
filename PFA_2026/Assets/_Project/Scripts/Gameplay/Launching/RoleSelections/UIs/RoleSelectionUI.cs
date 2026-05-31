using System;
using System.Collections.Generic;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Core.Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Naussilus.Gameplay.RoleSelections
{
    public class RoleSelectionUI : MonoPhaseListener<RoleSelection>, IPointerClickHandler
    {
        [SerializeField] private CanvasGroup selectionGroup;
        [SerializeField] private CanvasGroup firstPlayerGroup;
        [SerializeField] private List<TextMeshProUGUI> explainations;
        [SerializeField] private Button continueButton;
        
        private RoleSelection currentPhase;

        protected override void OnEnable()
        {
            Debug.Log($"RoleSelectionUI started");
            selectionGroup.Hide();
            firstPlayerGroup.Hide();
            base.OnEnable();
        }
        
        public async void RevealText()
        {
            foreach (TextMeshProUGUI explaination in explainations)
            {
                explaination.maxVisibleCharacters = 0;
            }
            foreach (TextMeshProUGUI explaination in explainations)
            {
                for (int i = 0; i <= explaination.text.Length; i++)
                {
                    if (explaination.maxVisibleCharacters < explaination.text.Length)
                    {
                        explaination.maxVisibleCharacters = i;
                        await Awaitable.WaitForSecondsAsync(0.02f);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        protected override void OnPhaseBegin(RoleSelection phase)
        {
            Debug.Log($"RoleSelection phase started");
            currentPhase = phase;
            selectionGroup.Show();
            RevealText();
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.ManagmentMusic(true);
                SoundDesignManager.instance.PlaySound(SoundsEnum.Writing);
            }
            continueButton.onClick.AddListener(OnClick);
            base.OnPhaseBegin(phase);
        }
        
        protected override void OnPhaseEnd(RoleSelection phase)
        {
            currentPhase = null;
            selectionGroup.Hide();
            firstPlayerGroup.Hide();
            continueButton.onClick.RemoveAllListeners();
            base.OnPhaseEnd(phase);
        }

        public bool AllRead()
        {
            foreach (TextMeshProUGUI explaination in explainations)
            {
                if (explaination.maxVisibleCharacters < explaination.text.Length)
                {
                    return false;
                }
            }
            return true;
        }
        
        public async void OnPointerClick(PointerEventData eventData)
        {
            try
            {
                if (AllRead())
                {
                    selectionGroup.Hide();
                    var intro = new IntroductionPhase();
                    await intro.Run();
                    firstPlayerGroup.Show();
                }
                else
                {
                    foreach (TextMeshProUGUI explaination in explainations)
                    {
                        explaination.maxVisibleCharacters = explaination.text.Length;
                    }
                }
                
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void OnClick()
        {
            currentPhase.SetResult(true);
        }
    }
}