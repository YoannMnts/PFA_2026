using Helteix.Tools.UI;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SummaryTextUI : UIItem<string>
    {
        [SerializeField] private TMP_Text summaryText;
        
        protected override void SyncUI(string current)
        {
            summaryText.text = current;
        }

        protected override void ClearUI()
        {
            summaryText.text = string.Empty;
        }
    }
}