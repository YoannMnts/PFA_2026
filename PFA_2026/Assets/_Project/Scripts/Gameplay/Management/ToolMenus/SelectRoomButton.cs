using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Gameplay.Buttons;
using Naussilus.Gameplay.Interactions;

namespace Naussilus.Gameplay
{
    public class SelectRoomButton : MonoPhaseListener<ManagementPhase>, IInteractable
    {
        private ActionPoint currentActionPoint;
    
        private ManagementPhase currentPhase;
        private SelectRoomForShip selectRoomForShip;
        public int Priority { get; private set; } = 10;

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

            if (selectRoomForShip != null && selectRoomForShip.IsRunning())
                selectRoomForShip.Cancel();
            
            base.OnPhaseEnd(phase);
        }

        public void OnShipClicked()
        {
            //var selectRoomForShip = new SelectRoomForShip(currentActionPoint, currentPhase);
            //selectRoomForShip.RunAndForget();
        }

        public async void Interact(PlayerInteractions playerInteractions)
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(this.gameObject);
            }
            selectRoomForShip = new SelectRoomForShip(currentActionPoint, currentPhase);
            selectRoomForShip.RunAndForget();
        }
    }
}