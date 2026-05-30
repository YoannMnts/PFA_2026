using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Naussilus.Gameplay.RoleSelections
{
    public class RoleSelectionUI : MonoPhaseListener<RoleSelection>, IPointerClickHandler
    {
        [SerializeField] private CanvasGroup selectionGroup;
        [SerializeField] private CanvasGroup firstPlayerGroup;
        
        private RoleSelection currentPhase;

        private void Start()
        {
            selectionGroup.Hide();
            firstPlayerGroup.Hide();
        }

        protected override void OnPhaseBegin(RoleSelection phase)
        {
            currentPhase = phase;
            selectionGroup.Show();
            firstPlayerGroup.Show();
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(RoleSelection phase)
        {
            currentPhase = null;
            selectionGroup.Hide();
            firstPlayerGroup.Hide();
            base.OnPhaseEnd(phase);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            selectionGroup.Hide();
        }

        public void OnClick()
        {
            currentPhase.SetResult(true);
        }
    }
}