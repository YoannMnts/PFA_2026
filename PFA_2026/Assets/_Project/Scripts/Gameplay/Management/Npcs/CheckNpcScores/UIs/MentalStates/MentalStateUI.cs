using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.MentalStates
{
    public class MentalStateUI : UIItem<MentalState>
    {
        [SerializeField]
        private TMP_Text mentalStateText;

        [SerializeField] 
        private Image fillBar;
        
        protected override void SyncUI(MentalState current)
        {
            mentalStateText.text = current.Name;
            fillBar.fillAmount = current.Amount / 20f;
        }

        protected override void ClearUI()
        {
            mentalStateText.text = string.Empty;
        }
    }
}