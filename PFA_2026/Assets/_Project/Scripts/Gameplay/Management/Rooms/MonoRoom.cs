using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managements;
using Naussilus.Core.Managers.Rooms;
using Naussilus.Gameplay.Player.Interactions;
using UnityEngine;

namespace Rooms
{
    public class MonoRoom: MonoPhaseListener<ManagementPhase>, IInteractable
    {
        public int Priority { get; private set; } = 0;
        
        [field: SerializeField] 
        public RoomData RoomData { get; private set; }

        [field: SerializeField]
        public Transform[] NpcSlots { get; private set; }
        private Room Room => RoomManager.TryGetRoom(RoomData.GUID);
        
        public ActionPoint CurrentActionPoint { get; private set; }
        
        public ManagementPhase CurrentPhase { get; private set; }
        
        private SelectActionForRoom selectActionForRoom;
        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            CurrentActionPoint = phase.CurrentActionPoint;
            CurrentPhase = phase;
            
            base.OnPhaseBegin(phase);
        }
        
        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            CurrentPhase = null;
            base.OnPhaseEnd(phase);
        }
        
        public void Interact(PlayerInteractions playerInteractions)
        {
            if (CurrentPhase == null)
                return;

            if (selectActionForRoom != null && selectActionForRoom.CurrentRoom == Room)
            {
                selectActionForRoom.Cancel();
                selectActionForRoom = null;
            }
            
            selectActionForRoom = new SelectActionForRoom(Room, CurrentActionPoint);
            selectActionForRoom.RunAndForget();
        }

        public bool IsInteractable()
        {
            return true;        
        }
    }
}