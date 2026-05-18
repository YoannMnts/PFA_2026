using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay.MentalStates
{
    public class MentalStateUI : UIItem<MentalState>
    {
        [SerializeField]
        private TMP_Text mentalStateText;
        
        protected override void SyncUI(MentalState current)
        {
            mentalStateText.text = current.Data.Name;
        }

        protected override void ClearUI()
        {
            mentalStateText.text = string.Empty;
        }
    }
}