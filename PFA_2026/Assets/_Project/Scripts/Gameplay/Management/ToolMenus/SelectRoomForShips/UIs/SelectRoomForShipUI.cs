using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Sounds;
using Naussilus.Gameplay.Buttons;
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
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.PlaySound(SoundsEnum.OpenTab);
            }
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

        public async void Cancel(GameObject button)
        {
            if (Current != null)
            {
                if (ButtonFeedbacksManager.instance != null)
                {
                    await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(button);
                }
                Current.SetResult(null);
                if (SoundDesignManager.instance != null)
                {
                    SoundDesignManager.instance.PlaySound(SoundsEnum.OpenTab);
                }
            }
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