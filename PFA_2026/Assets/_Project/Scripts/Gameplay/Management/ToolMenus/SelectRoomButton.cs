using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Gameplay.Interactions;

namespace Naussilus.Gameplay
{
    public class SelectRoomButton : MonoPhaseListener<ManagementPhase>, IInteractable
    {
        private ActionPoint currentActionPoint;
    
        private ManagementPhase currentPhase;
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
        
            base.OnPhaseEnd(phase);
        }

        public void OnShipClicked()
        {
            //var selectRoomForShip = new SelectRoomForShip(currentActionPoint, currentPhase);
            //selectRoomForShip.RunAndForget();
        }

        public void Interact(PlayerInteractions playerInteractions)
        {
            var selectRoomForShip = new SelectRoomForShip(currentActionPoint, currentPhase);
            selectRoomForShip.RunAndForget();
        }
    }
}