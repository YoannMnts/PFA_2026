using Helteix.Tools.UI;
using Naussilus.Core;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class ConsequenceSummaryUI : UIItem<Consequence>
    {
        [SerializeField] private SummaryTextUIList consequenceSummaryText;
        
        protected override void SyncUI(Consequence current)
        {
            var allText = current.Text;
            consequenceSummaryText.Connect(allText);
        }

        protected override void ClearUI()
        {
            consequenceSummaryText.Disconnect();
        }
    }
}