using Helteix.Tools.UI;
using Naussilus.Core;
using UnityEngine;


namespace Naussilus.Gameplay
{
    public class ActionConsequenceUI : UIItem<Consequence>
    {
        [SerializeField]
        private ConsequenceTextUIList consequenceTextUIList;

        private bool hasText;

        protected override void SyncUI(Consequence current)
        {
            var allText = current.Text;
            if (allText == null)
                return;
            hasText = allText.Length > 0;
            if (hasText)
            {
                consequenceTextUIList.Connect(allText);
            }
        }

        protected override void ClearUI()
        {
            if(!hasText)
                return;
            consequenceTextUIList.Disconnect();
        }
    }
}