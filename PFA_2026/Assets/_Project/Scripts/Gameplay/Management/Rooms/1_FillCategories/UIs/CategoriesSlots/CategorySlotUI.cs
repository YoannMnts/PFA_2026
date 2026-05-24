using System;
using Helteix.Tools.UI;
using Naussilus.Gameplay.CategoriesTitles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Naussilus.Gameplay.CategoriesSlots
{
    public class CategorySlotUI : UIItem<CategoryNpcSlot>
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;
        
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
            text.text = npc?.Name;
            if (current.NpcExpression.TryGetExpression(npc, out var sprite))
            {
                icon.sprite = sprite;
            }
        }

        protected override void ClearUI()
        {
            icon.sprite = null;
            text.text = string.Empty;
        }

        private void OnClick()
        {
            categoryUI.OnClicked(Current.CurrentNpc);
        }
    }
}