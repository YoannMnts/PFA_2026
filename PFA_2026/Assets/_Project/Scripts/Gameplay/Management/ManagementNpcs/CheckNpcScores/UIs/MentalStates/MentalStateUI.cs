using Helteix.Tools.UI;
using Naussilus.Core.NpcDatas;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class MentalStateUI : UIItem<NpcMentalState>
    {
        [SerializeField]
        private TMP_Text mentalStateText;
        
        [SerializeField]
        private TMP_Text valueText;
        
        protected override void SyncUI(NpcMentalState current)
        {
            mentalStateText.text = current.MentalState.ToString();
            valueText.text = current.Amount.ToString();
        }

        protected override void ClearUI()
        {
            mentalStateText.text = string.Empty;
            valueText.text = string.Empty;
        }
    }
}