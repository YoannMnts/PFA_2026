using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Gameplay.Buttons;
using Naussilus.Gameplay.Interactions;

namespace Naussilus.Gameplay
{
    public class OptionButton : MonoPhaseListener<ManagementPhase>, IInteractable
    {
        public int Priority { get; private set; } = 10;
        
        private ActionPoint currentActionPoint;
    
        private ManagementPhase currentPhase;
        private OptionMenu optionMenu;

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            currentPhase = phase;
            currentActionPoint = phase.CurrentActionPoint;
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            currentPhase = null;
            currentActionPoint = null;

            if (optionMenu != null && optionMenu.IsRunning())
                optionMenu.Cancel();
            
            base.OnPhaseEnd(phase);
        }
        public async void Interact(PlayerInteractions playerInteractions)
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(this.gameObject);
            }
            optionMenu = new OptionMenu();
            optionMenu.RunAndForget();
            
        }
    }
}