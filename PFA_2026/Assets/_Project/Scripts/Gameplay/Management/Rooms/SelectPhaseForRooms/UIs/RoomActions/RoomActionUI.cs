using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class RoomActionUI : UIItem<RoomAction>
    {
        private SelectActionForRoomUI selectActionForRoomUI;

        [SerializeField]
        private TMP_Text titleText;
        [SerializeField]
        private TMP_Text costText;

        private void Start()
        {
            selectActionForRoomUI = GetComponentInParent<SelectActionForRoomUI>();
        }

        protected override void SyncUI(RoomAction current)
        {
            titleText.text = current.Name;
            costText.text = current.Cost.ToString();
        }

        protected override void ClearUI()
        {
            titleText.text = string.Empty;
            costText.text = string.Empty;
        }

        public void OnClicked()
        {
            selectActionForRoomUI.ChooseAction(Current);
        }
    }
}