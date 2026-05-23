using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class RoomActionUI : UIItem<RoomAction>
    {
        private SelectActionForRoomUI selectActionForRoomUI;

        [SerializeField]
        private TMP_Text titleText;
        

        private void Start()
        {
            selectActionForRoomUI = GetComponentInParent<SelectActionForRoomUI>();
        }

        protected override void SyncUI(RoomAction current)
        {
            titleText.text = current.Name;
        }

        protected override void ClearUI()
        {
            titleText.text = string.Empty;
        }

        public void OnClicked()
        {
            selectActionForRoomUI.ChooseAction(Current);
        }
    }
}