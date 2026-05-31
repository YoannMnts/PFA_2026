using System;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Naussilus.Gameplay.RoleSelections
{
    public class RoleSelectionUI : MonoPhaseListener<RoleSelection>, IPointerClickHandler
    {
        [SerializeField] private CanvasGroup selectionGroup;
        [SerializeField] private CanvasGroup firstPlayerGroup;
        [SerializeField] private Button continueButton;
        
        private RoleSelection currentPhase;

        protected override void OnEnable()
        {
            Debug.Log($"RoleSelectionUI started");
            selectionGroup.Hide();
            firstPlayerGroup.Hide();
            base.OnEnable();
        }
        

        protected override void OnPhaseBegin(RoleSelection phase)
        {
            Debug.Log($"RoleSelection phase started");
            currentPhase = phase;
            selectionGroup.Show();
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

        public async void OnPointerClick(PointerEventData eventData)
        {
            try
            {
                selectionGroup.Hide();
                var intro = new IntroductionPhase();
                await intro.Run();
                firstPlayerGroup.Show();
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