using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managements;
using Naussilus.Core.Managers.Rooms;
using Naussilus.Gameplay.Interactions;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class MonoRoom: MonoPhaseListener<ManagementPhase>, IInteractable
    {
        public int Priority { get; private set; } = 0;
        
        [field: SerializeField] 
        public RoomData RoomData { get; private set; }
        
        [field: SerializeField]
        public MonoCineCamera CineCamera { get; private set; }

        [field: SerializeField]
        public RoomNpcSlot[] NpcSlots { get; private set; }
        private Room Room => RoomManager.TryGetRoom(RoomData.GUID);
        
        public ActionPoint CurrentActionPoint { get; private set; }
        
        public ManagementPhase CurrentPhase { get; private set; }
        
        private SelectActionForRoom selectActionForRoom;

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            CurrentActionPoint = phase.CurrentActionPoint;
            CurrentPhase = phase;
            phase.OnRoomSelected += TrySelectRoom;
            
            base.OnPhaseBegin(phase);
        }
        
        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            CurrentPhase = null;
            phase.OnRoomSelected -= TrySelectRoom;
            
            base.OnPhaseEnd(phase);
        }

        private void TrySelectRoom(Room room)
        {
            if (Room != room)
                return;
                
            Interact(null);
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

            if (Room.IsInCountdown)
            {
                var currentlyInAction = new CurrentlyInAction(Room, CineCamera);
                currentlyInAction.RunAndForget();
                return;
            }
            
            selectActionForRoom = new SelectActionForRoom(Room, CurrentActionPoint, NpcSlots, CineCamera);
            selectActionForRoom.RunAndForget();
        }

        public bool IsInteractable()
        {
            return true;        
        }
    }
}