using System.Linq;
using Helteix.Tools.UI;
using Naussilus.Core.Managements.RoomDatas.ActionDatas;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    public class RoomActionUI : UIItem<ActionData>
    {
        [HideInInspector]
        public SelectActionForRoomUI selectActionForRoomUI;

        [SerializeField]
        private TMP_Text titleText;
        [SerializeField]
        private TMP_Text descriptionText;
        
        
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