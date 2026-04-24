using Helteix.Tools.UI;
using Naussilus.Core.NpcDatas;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class BehaviorUI : UIItem<Behavior>
    {
        [SerializeField]
        private TMP_Text behaviorText;
        
        [SerializeField]
        private TMP_Text valueText;
        protected override void SyncUI(Behavior current)
        {
            behaviorText.text = current.Name;
            valueText.text = current.Amount.ToString();
        }

        protected override void ClearUI()
        {
            behaviorText.text = string.Empty;
            valueText.text = string.Empty;
        }
    }
}