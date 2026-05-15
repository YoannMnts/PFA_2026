using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SelectRoomForShipUI: MonoPhaseListener<SelectRoomForShip>
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private ShipRoomUIList shipUIList;

        public SelectRoomForShip Current { get; private set; }

        private ActionPoint currentActionPoint;
        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(SelectRoomForShip phase)
        {
            if (Current != null)
                return;
            
            Current = phase;
            group.Show();
            shipUIList.Connect(phase.CurrentRooms);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectRoomForShip phase)
        {
            if (Current != phase)
                return;
            
            Current = null;
            shipUIList.Disconnect();
            group.Hide();
            
            base.OnPhaseEnd(phase);
        }

        public void Cancel()
        {
            if (Current != null)
                Current.SetResult(null);
        }

        public void ChooseRoom(Room room)
        {
            if (Current == null)
                return;
        
            Current.CurrentPhase.SelectRoom(room);
            
            Current.SetResult(room);
        }
    }
}