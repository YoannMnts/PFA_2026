using System;
using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Core.Managements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class ShipRoomUI : UIItem<Room>
    {
        private SelectRoomForShipUI selectRoomForShipUI;
        private Room currentRoom;
        
        [SerializeField] private Button button;
        [SerializeField] private Image sprite;
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            selectRoomForShipUI = GetComponentInParent<SelectRoomForShipUI>();
        }

        protected override void SyncUI(Room current)
        {
            currentRoom = current;
            text.text = current.Name;
            //sprite.sprite = current.Sprite;
            button.onClick.AddListener(OnClicked);
        }

        protected override void ClearUI()
        {
            text.text = string.Empty;
            button.onClick.RemoveAllListeners();
        }

        public void OnClicked()
        {
            selectRoomForShipUI.ChooseRoom(currentRoom);
        }
    }
}