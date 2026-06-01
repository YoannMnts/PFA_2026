using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Gameplay.Interactions;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.UIs
{
    public class TutorialPhaseUI : MonoPhaseListener<TutorialPhase>, IInteractable
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private Button invisibleButton;
        private TutorialPhase current;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(TutorialPhase phase)
        {
            if (current != null)
                current.Cancel();
            
            current = phase;
            group.Show();
            invisibleButton.onClick.AddListener(OnButtonClicked);
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(TutorialPhase phase)
        {
            if(current == null)
                return;
            
            group.Hide();
            invisibleButton.onClick.RemoveListener(OnButtonClicked);
            base.OnPhaseEnd(phase);
        }

        private void OnButtonClicked()
        {
            //current.SetResult(false);
        }

        public int Priority { get; private set; } = 20;
        public void Interact(PlayerInteractions playerInteractions)
        {
            Debug.Log("aaaaaaaaaaaaaaaa");
            current.SetResult(false);
        }
    }
}