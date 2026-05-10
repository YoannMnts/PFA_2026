using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel
{
    public class ConsequenceSummaryUI : UIItem<Consequence>
    {
        [SerializeField] private TMP_Text consequenceSummaryText;
        
        protected override void SyncUI(Consequence current)
        {
            for (int i = 0; i < current.Text.Length; i++)
            {
                consequenceSummaryText.text += current.Text[i];
            }
            Debug.Log($"consequenceSummaryText length : {current.Text.Length}");
        }

        protected override void ClearUI()
        {
            consequenceSummaryText.text = string.Empty;
        }
    }
}