using System;
using System.Collections.Generic;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Core.Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Naussilus.Gameplay
{
    public class IntroductionPhaseUI : MonoPhaseListener<IntroductionPhase>, IPointerClickHandler
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private List<TextMeshProUGUI> explainations;
        
        private IntroductionPhase currentPhase;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(IntroductionPhase phase)
        {
           currentPhase = phase;
           group.Show();
           RevealText();
           if (SoundDesignManager.instance != null)
           {
               SoundDesignManager.instance.PlaySound(SoundsEnum.Writing);
           }
            base.OnPhaseBegin(phase);
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
        protected override void OnPhaseEnd(IntroductionPhase phase)
        {
            currentPhase = null;
            group.Hide();
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
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (AllRead())
            {
                currentPhase.SetResult(true);
            }
            else
            {
                foreach (TextMeshProUGUI explaination in explainations)
                {
                    explaination.maxVisibleCharacters = explaination.text.Length;
                }
            }
        }
    }
}