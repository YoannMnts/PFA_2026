using System;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managements;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;

namespace Rooms
{
    public class MonoRoom: MonoPhaseListener<ManagementPhase>
    {
        public event Action OnMenuClose;
        
        [field: SerializeField] 
        public RoomData RoomData { get; private set; }

        private Room Room => RoomManager.TryGetRoom(RoomData.GUID);
        
        public ActionPoint CurrentActionPoint { get; private set; }
        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            CurrentActionPoint = phase.CurrentActionPoint;
            base.OnPhaseBegin(phase);
        }

        public void OnClick()
        {
            var selectActionForRoom = new SelectActionForRoom(Room, CurrentActionPoint);
            selectActionForRoom.RunAndForget();
            selectActionForRoom.OnCloseMenu += MenuClose;
        }

        public void MenuClose()
        {
            OnMenuClose?.Invoke();
        }
    }
}