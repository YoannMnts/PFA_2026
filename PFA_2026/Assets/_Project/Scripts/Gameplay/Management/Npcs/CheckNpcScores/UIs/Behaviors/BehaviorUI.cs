using Helteix.Tools.UI;
using Naussilus.Core.NpcDatas;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class BehaviorUI : UIItem<NpcBehavior>
    {
        [SerializeField]
        private TMP_Text behaviorText;
        
        [SerializeField]
        private TMP_Text valueText;
        protected override void SyncUI(NpcBehavior current)
        {
            behaviorText.text = current.Stat.ToString();
            valueText.text = current.Amount.ToString();
        }

        protected override void ClearUI()
        {
            behaviorText.text = string.Empty;
            valueText.text = string.Empty;
        }
    }
}