using Helteix.Tools.UI;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class ConsequenceTextUI : UIItem<string>
    {
        [SerializeField]
        private TMP_Text consequenceText;
        protected override void SyncUI(string current)
        {
            consequenceText.text = current;
        }

        protected override void ClearUI()
        {
            consequenceText.text = string.Empty;
        }
    }
}