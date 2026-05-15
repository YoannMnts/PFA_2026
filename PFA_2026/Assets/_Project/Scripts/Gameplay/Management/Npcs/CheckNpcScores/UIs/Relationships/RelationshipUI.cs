using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class RelationshipUI : UIItem<NpcRelationship>
    {
        [SerializeField] private TMP_Text otherNameText;
        
        protected override void SyncUI(NpcRelationship current)
        {
            otherNameText.text = current.Npc.Name;
        }

        protected override void ClearUI()
        {
            otherNameText.text = string.Empty;
        }
    }
}