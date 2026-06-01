using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Gameplay.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class RoomActionUI : UIItem<RoomAction>
    {
        private SelectActionForRoomUI selectActionForRoomUI;

        [SerializeField]
        private TMP_Text titleText;
        [SerializeField]
        private TMP_Text actionPoint;
        [SerializeField]
        private Button activityButton;

        private void Start()
        {
            //selectActionForRoomUI = GetComponentInParent<SelectActionForRoomUI>();
        }

        protected override void SyncUI(RoomAction current)
        {
            selectActionForRoomUI = GetComponentInParent<SelectActionForRoomUI>();
            titleText.text = current.Name;
            actionPoint.text = current.Cost.ToString();
            activityButton.interactable = current.Cost <= selectActionForRoomUI.ActionPoint.Value;
            activityButton.onClick.AddListener(OnClicked);
        }

        protected override void ClearUI()
        {
            titleText.text = string.Empty;
            actionPoint.text = string.Empty;
            activityButton.interactable = false;
            activityButton.onClick.RemoveAllListeners();
        }

        private void OnClicked()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(this.gameObject);
            }
            selectActionForRoomUI.ChooseAction(Current);
        }
    }
}