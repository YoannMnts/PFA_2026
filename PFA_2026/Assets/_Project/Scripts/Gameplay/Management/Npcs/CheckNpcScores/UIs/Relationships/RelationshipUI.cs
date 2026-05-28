using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class RelationshipUI : UIItem<NpcRelationship>
    {
        [SerializeField] private TMP_Text otherNameText;

        [SerializeField] private Image fillBar;
        
        protected override void SyncUI(NpcRelationship current)
        {
            otherNameText.text = current.Npc.Name;
            fillBar.fillAmount = current.Amount / 20f;
        }

        protected override void ClearUI()
        {
            otherNameText.text = string.Empty;
            fillBar.fillAmount = 1f;
        }
    }
}