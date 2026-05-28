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
        [SerializeField] private Button button;

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
            button.interactable = current.CurrentCategory == null;
        }

        protected override void ClearUI()
        {
            icon.sprite = null;
            button.onClick.RemoveAllListeners();
            button.interactable = true;
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