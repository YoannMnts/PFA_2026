using System;
using Helteix.Tools.UI;
using Naussilus.Gameplay.CategoriesTitles;
using UnityEngine;
using UnityEngine.UI;


namespace Naussilus.Gameplay.CategoriesSlots
{
    public class CategorySlotUI : UIItem<CategoryNpcSlot>
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button button;
        
        private CategoryUI categoryUI;

        private void Awake()
        {
            categoryUI = GetComponentInParent<CategoryUI>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        protected override void SyncUI(CategoryNpcSlot current)
        {
            var npc = current.CurrentNpc;
            var expressions = current.NpcExpression.Expressions;
            for (int i = 0; i < expressions?.Length; i++)
            {
                if (expressions[i].Npc == npc)
                    icon.sprite = expressions[i].Sprite;
            }

        }

        protected override void ClearUI()
        {
            icon.sprite = null;
        }

        private void OnClick()
        {
            categoryUI.OnClicked(Current.CurrentNpc);
        }
    }
}