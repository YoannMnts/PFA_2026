using System;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Naussilus.Gameplay
{
    public class IntroductionPhaseUI : MonoPhaseListener<IntroductionPhase>, IPointerClickHandler
    {
        [SerializeField] private CanvasGroup group;
        
        private IntroductionPhase currentPhase;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(IntroductionPhase phase)
        {
           currentPhase = phase;
           group.Show();
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(IntroductionPhase phase)
        {
            currentPhase = null;
            group.Hide();
            base.OnPhaseEnd(phase);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            currentPhase.SetResult(true);
        }
    }
}