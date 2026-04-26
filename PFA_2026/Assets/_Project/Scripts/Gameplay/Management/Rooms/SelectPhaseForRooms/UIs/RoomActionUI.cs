using Helteix.Tools.UI;
using Naussilus.Core.Managements.RoomDatas.ActionDatas;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Rooms
{
    public class RoomActionUI : UIItem<ActionData>
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

        protected override void SyncUI(ActionData current)
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