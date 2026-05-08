using Helteix.Tools.UI;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class ConsequenceTextUI : UIItem<string[]>
    {
        [SerializeField]
        private TMP_Text textField;
        
        protected override void SyncUI(string[] current)
        {
            textField.text = current[0];
        }

        protected override void ClearUI()
        {
            textField.text = string.Empty;
        }
    }
}