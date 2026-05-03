using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms
{
    public class SlotNpcUI : UIItem<Npc>
    {
        private SelectNpcForCategoryUI selectNpcForCategoryUI;

        [SerializeField] private TMP_Text title;
        
        [SerializeField] private Image image;

        private void Start()
        {
            selectNpcForCategoryUI = GetComponentInParent<SelectNpcForCategoryUI>();
        }

        protected override void SyncUI(Npc current)
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