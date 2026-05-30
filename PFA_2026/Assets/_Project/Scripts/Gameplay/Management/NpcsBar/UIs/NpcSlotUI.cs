using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Gameplay.Buttons;
using Naussilus.Gameplay.Interactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class NpcSlotUI : UIItem<Npc>, IInteractable
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image background;
        [SerializeField] private Button button;
        [SerializeField] private Sprite defaultBackground;
        [SerializeField] private Sprite lockedBackground;

        public int Priority { get; private set; } = 10;
        private NpcBarUI npcBar;

        private void Start()
        {
            npcBar = GetComponentInParent<NpcBarUI>();
        }

        protected override void SyncUI(Npc current)
        {
            icon.sprite = current.DefaultIcon;
            button.onClick.AddListener(OnClick);
            background.sprite = defaultBackground;
            current.OnAddedInSlot += OnAddedInSlot;
            current.OnRemoveSlot += OnRemoveSlot;
        }


        protected override void ClearUI()
        {
            icon.sprite = null;
            button.onClick.RemoveAllListeners();
            button.interactable = true;
            if (Current == null) 
                return;
            Current.OnAddedInSlot -= OnAddedInSlot;
            Current.OnRemoveSlot -= OnRemoveSlot;
        }

        private void OnAddedInSlot(CategoryNpcSlot obj)
        {
            background.sprite = Current?.CurrentCategory != null ? lockedBackground : defaultBackground;
        }

        private void OnRemoveSlot()
        {
            background.sprite = Current?.CurrentCategory != null ? lockedBackground : defaultBackground;
        }
        
        private void OnClick()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(button.gameObject);
            }
            npcBar.OnClick(Current);
        }

        public void Interact(PlayerInteractions playerInteractions)
        {
            //npcBar.OnClick(Current);
        }
    }
}