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
        
        private Room Room => RoomManager.TryGetRoom(RoomData.GUID);
        
        public ActionPoint CurrentActionPoint { get; private set; }
        
        public ManagementPhase CurrentPhase { get; private set; }
        
        private SelectActionForRoom selectActionForRoom;
        private CurrentlyInAction currentlyInAction;
        private CaptainRoom captainRoomPhase;


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
            
            if (selectActionForRoom != null && selectActionForRoom.IsRunning())
                selectActionForRoom.Cancel();
            
            if (currentlyInAction != null && currentlyInAction.IsRunning())
                currentlyInAction.Cancel();
            
            if (captainRoomPhase != null && captainRoomPhase.IsRunning())
                captainRoomPhase.Cancel();
            
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
            Debug.Log($"Interacting room {Room.Name}");
            if (CurrentPhase == null)
                return;
            

            if (selectActionForRoom != null && selectActionForRoom.IsRunning() && selectActionForRoom.CurrentRoom == Room)
            {
                selectActionForRoom.Cancel();
                selectActionForRoom = null;
            }

            if (Room.IsInCountdown)
            {
                currentlyInAction = new CurrentlyInAction(Room, CineCamera);
                currentlyInAction.RunAndForget();
                return;
            }

            if (Room.Name == "Cabine du capitaine")
            {
                if (captainRoomPhase != null && captainRoomPhase.IsRunning())
                {
                    captainRoomPhase.Cancel();
                    captainRoomPhase = null;
                }
                captainRoomPhase = new CaptainRoom(CurrentPhase, CineCamera);
                captainRoomPhase.RunAndForget();
                return;
            }
            selectActionForRoom = new SelectActionForRoom(Room, CurrentActionPoint, CineCamera);
            selectActionForRoom.RunAndForget();
        }

        public bool IsInteractable()
        {
            return true;        
        }
    }
}