using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Core.NpcDatas;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class MentalStateUI : UIItem<MentalState>
    {
        [SerializeField]
        private TMP_Text mentalStateText;
        
        [SerializeField]
        private TMP_Text valueText;
        
        protected override void SyncUI(MentalState current)
        {
            mentalStateText.text = current.Data.Name;
            valueText.text = current.Amount.ToString();
        }

        protected override void ClearUI()
        {
            mentalStateText.text = string.Empty;
            valueText.text = string.Empty;
        }
    }
}