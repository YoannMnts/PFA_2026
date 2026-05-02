using Helteix.Tools.UI;
using Naussilus.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Rooms
{
    [RequireComponent(typeof(Button))]
    public class CategorySlotUI : UIItem<Npc>
    {
        [SerializeField]
        private Image icon;
        
        private CategoryUI categoryUI;
        private Button button;

        private void OnEnable()
        {
            categoryUI = GetComponentInParent<CategoryUI>();
            button = GetComponent<Button>();
        }

        protected override void SyncUI(Npc current)
        {
            icon.sprite = current?.CategoryIcon;

            if (categoryUI.Current.ObligateNpcs != null)
                return;
            
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            categoryUI.OnClicked(this);
        }

        protected override void ClearUI()
        {
            button?.onClick.RemoveAllListeners();
        }
    }
}