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

        private void Start()
        {
            //selectionGroup.Hide();
            //firstPlayerGroup.Hide();
        }

        protected override void OnPhaseBegin(RoleSelection phase)
        {
            currentPhase = phase;
            selectionGroup.Show();
            firstPlayerGroup.Show();
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

        public void OnPointerClick(PointerEventData eventData)
        {
            selectionGroup.Hide();
        }

        private void OnClick()
        {
            currentPhase.SetResult(true);
        }
    }
}