using System;
using Helteix.Tools.UI;
using Naussilus.Gameplay.Buttons;
using Naussilus.Gameplay.CategoriesTitles;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Naussilus.Gameplay.CategoriesSlots
{
    public class CategorySlotUI : UIItem<CategoryNpcSlot>
    {
        [SerializeField] private Image npcIcon;
        [SerializeField] private Image background;
        [SerializeField] private Button button;
        [SerializeField] private Sprite defaultBGIcon;
        [SerializeField] private Sprite selectedBGIcon;
        
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
            var npc = current?.CurrentNpc;
            npcIcon.sprite = npc?.DefaultIcon;
            var color = npcIcon.color;
            color.a = npc != null ? 1f : 0f;
            npcIcon.color = color;
            background.sprite = defaultBGIcon;
        }

        protected override void ClearUI()
        {
            npcIcon.sprite = null;
            background.sprite = null;
        }

        private void OnClick()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(button.gameObject);
            }
            categoryUI.OnClicked(Current.CurrentNpc);
            npcIcon.sprite = defaultBGIcon;
        }

        public bool TrySelectSlot()
        {
            if (Current.CurrentNpc == null)
            {
                background.sprite = selectedBGIcon;
                return false;
            }
            background.sprite = defaultBGIcon;
            return true;
        }
    }
}