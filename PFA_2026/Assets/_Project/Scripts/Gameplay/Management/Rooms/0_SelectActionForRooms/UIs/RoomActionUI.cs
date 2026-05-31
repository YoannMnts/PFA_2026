using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Gameplay.Buttons;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class RoomActionUI : UIItem<RoomAction>
    {
        private SelectActionForRoomUI selectActionForRoomUI;

        [SerializeField]
        private TMP_Text titleText;
        [SerializeField]
        private TMP_Text actionPoint;
        

        private void Start()
        {
            selectActionForRoomUI = GetComponentInParent<SelectActionForRoomUI>();
        }

        protected override void SyncUI(RoomAction current)
        {
            titleText.text = current.Name;
            actionPoint.text = current.Cost.ToString();
        }

        protected override void ClearUI()
        {
            titleText.text = string.Empty;
            actionPoint.text = string.Empty;
        }

        public void OnClicked()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(this.gameObject);
            }
            selectActionForRoomUI.ChooseAction(Current);
        }
    }
}