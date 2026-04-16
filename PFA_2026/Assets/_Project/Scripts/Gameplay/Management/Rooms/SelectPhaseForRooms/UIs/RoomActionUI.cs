using Helteix.Tools.UI;
using Naussilus.Core.Managements.RoomDatas.ActionDatas;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Rooms
{
    public class RoomActionUI : UIItem<ActionData>
    {
        private SelectActionForRoomUI selectActionForRoomUI;

        [SerializeField]
        private TMP_Text titleText;
        [SerializeField]
        private TMP_Text descriptionText;

        private void Start()
        {
            selectActionForRoomUI = GetComponentInParent<SelectActionForRoomUI>();
        }

        protected override void SyncUI(ActionData current)
        {
            titleText.text = current.Name;
            descriptionText.text = "Ya rien encore";
        }

        protected override void ClearUI()
        {
            titleText.text = string.Empty;
            descriptionText.text = string.Empty;
        }

        public void OnClicked()
        {
            selectActionForRoomUI.ChooseAction(Current);
        }
    }
}