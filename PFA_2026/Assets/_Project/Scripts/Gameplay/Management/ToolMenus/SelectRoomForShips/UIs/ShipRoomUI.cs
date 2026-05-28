using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Gameplay.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class ShipRoomUI : UIItem<Room>
    {
        private SelectRoomForShipUI selectRoomForShipUI;
        private Room currentRoom;
        
        [SerializeField] private Button button;
        [SerializeField] private Image sprite;

        private void Start()
        {
            selectRoomForShipUI = GetComponentInParent<SelectRoomForShipUI>();
        }

        protected override void SyncUI(Room current)
        {
            currentRoom = current;
            sprite.sprite = current.Icon;
            button.onClick.AddListener(OnClicked);
        }

        protected override void ClearUI()
        {
            button.onClick.RemoveAllListeners();
        }

        public void OnClicked()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(button.gameObject);
            }
            selectRoomForShipUI.ChooseRoom(currentRoom);
        }
    }
}