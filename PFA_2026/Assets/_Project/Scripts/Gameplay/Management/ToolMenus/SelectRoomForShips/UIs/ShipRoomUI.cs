using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Gameplay.Buttons;
using Naussilus.Gameplay.Interactions;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class ShipRoomUI : UIItem<Room>, IInteractable
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

        public async void OnClicked()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(button.gameObject);
            }
            selectRoomForShipUI.ChooseRoom(currentRoom);
        }

        public int Priority { get; private set; } = 10;
        public void Interact(PlayerInteractions playerInteractions)
        {
            
        }
    }
}