using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class NpcSlotUI : UIItem<Npc>
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text npcName;
        
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
            npcBar.OnClick(Current);
        }
    }
}