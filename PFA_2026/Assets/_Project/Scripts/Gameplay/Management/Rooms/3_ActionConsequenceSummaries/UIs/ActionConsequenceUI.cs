using Helteix.Tools.UI;
using Naussilus.Core;
using UnityEngine;


namespace Naussilus.Gameplay
{
    public class ActionConsequenceUI : UIItem<Consequence>
    {
        [SerializeField]
        private ConsequenceTextUIList consequenceTextUIList;
        
        protected override void SyncUI(Consequence current)
        {
            var allText = current.Text;
            if (allText == null)
                return;
            if (allText.Length > 0)
            {
                consequenceTextUIList.Connect(allText);
            }
        }

        protected override void ClearUI()
        {
            consequenceTextUIList.Disconnect();
        }
    }
}