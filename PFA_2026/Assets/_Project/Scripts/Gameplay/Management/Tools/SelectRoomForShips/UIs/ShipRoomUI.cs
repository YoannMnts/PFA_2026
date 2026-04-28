using System;
using Helteix.Tools.UI;
using Naussilus.Core.Managements.RoomDatas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class ShipRoomUI : UIItem<RoomData>
    {
        private SelectRoomForShipUI selectRoomForShipUI;
        private RoomData currentRoomData;
        
        [SerializeField] private Button button;
        [SerializeField] private Image sprite;
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            selectRoomForShipUI = GetComponentInParent<SelectRoomForShipUI>();
        }

        protected override void SyncUI(RoomData current)
        {
            currentRoomData = current;
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
            selectRoomForShipUI.ChooseRoom(currentRoomData);
        }
    }
}