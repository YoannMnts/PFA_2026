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
        private Image sprite;
        
        private CategoryUI categoryUI;
        private Button button;

        private void OnEnable()
        {
            categoryUI = GetComponentInParent<CategoryUI>();
            button = GetComponent<Button>();
        }

        protected override void SyncUI(Npc current)
        {
            if (current == null)
                return;
            
            button.onClick.AddListener(OnClick);
            categoryUI.Current.OnNpcAdded += SetSprite;
        }

        private void OnClick()
        {
            categoryUI.OnClicked(this);
        }

        private void SetSprite(Npc npc)
        {
            //sprite.sprite = current?.Sprite;
        }

        protected override void ClearUI()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}