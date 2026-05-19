using Helteix.Tools.UI;
using Naussilus.Core;
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
        [SerializeField] private TMP_Text npcName;

        public int Priority { get; private set; } = 10;
        private NpcBarUI npcBar;

        private void Start()
        {
            npcBar = GetComponentInParent<NpcBarUI>();
        }

        protected override void SyncUI(Npc current)
        {
            //icon.sprite = current.CategoryIcon;
            npcName.text = current.Name;
            button.onClick.AddListener(OnClick);
        }

        protected override void ClearUI()
        {
            //icon.sprite = null;
            npcName.text = string.Empty;
            button.onClick.RemoveAllListeners();
        }

        private void OnClick()
        {
            //npcBar.OnClick(Current);
        }

        public void Interact(PlayerInteractions playerInteractions)
        {
            npcBar.OnClick(Current);
        }
    }
}