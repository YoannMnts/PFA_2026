using System;
using Helteix.Tools.UI;
using Naussilus.Gameplay.Buttons;
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
            icon.sprite = npc?.DefaultIcon;
        }

        protected override void ClearUI()
        {
            icon.sprite = null;
        }

        private void OnClick()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(button.gameObject);
            }
            categoryUI.OnClicked(Current.CurrentNpc);
        }
    }
}