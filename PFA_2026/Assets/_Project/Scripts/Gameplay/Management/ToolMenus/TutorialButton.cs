using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Gameplay.Buttons;
using Naussilus.Gameplay.Interactions;

namespace Naussilus.Gameplay
{
    public class TutorialButton : MonoPhaseListener<ManagementPhase>, IInteractable
    {
        public int Priority { get; private set; } = 10;
        private TutorialPhase tutorialPhase;

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            if (tutorialPhase != null && tutorialPhase.IsRunning())
                tutorialPhase.Cancel();
            
            base.OnPhaseEnd(phase);
        }

        public async void Interact(PlayerInteractions playerInteractions)
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(this.gameObject);
            }
            tutorialPhase = new TutorialPhase();
            tutorialPhase.RunAndForget();
        }
    }
}