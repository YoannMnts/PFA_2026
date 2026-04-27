using Helteix.Tools.UI;
using Naussilus.Core.NpcDatas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Rooms
{
    public class SlotNpcUI : UIItem<NpcData>
    {
        private SelectNpcForCategoryUI selectNpcForCategoryUI;

        [SerializeField] private TMP_Text title;
        
        [SerializeField] private Image image;

        private void Start()
        {
            selectNpcForCategoryUI = GetComponentInParent<SelectNpcForCategoryUI>();
        }

        protected override void SyncUI(NpcData current)
        {
            //image.sprite = current.ActionSprite;
            title.text = current.Name;
        }

        protected override void ClearUI()
        {
            image.sprite = null;
        }

        public void OnClicked()
        {
            selectNpcForCategoryUI.ChooseNpc(Current);
        }
    }
}