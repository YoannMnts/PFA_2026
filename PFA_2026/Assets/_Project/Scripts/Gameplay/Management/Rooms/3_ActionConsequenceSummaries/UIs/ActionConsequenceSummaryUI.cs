using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms
{
    public class ActionConsequenceSummaryUI : MonoPhaseListener<ActionConsequenceSummary>
    {
        private ActionConsequenceSummary current;

        [SerializeField] private CanvasGroup group;

        [SerializeField] private ConsequenceTextUIList consequenceTextUIList;
        
        [SerializeField] private Button closeButton;
        
        [SerializeField] private Button applyButton;
        
        

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(ActionConsequenceSummary phase)
        {
            if(current != null)
                return;
            
            current = phase;
            group.Show();
            //consequenceTextUIList.Connect();
            closeButton.onClick.AddListener(Abort);
            applyButton.onClick.AddListener(Apply);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ActionConsequenceSummary phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            group.Hide();
            closeButton.onClick.RemoveAllListeners();
            applyButton.onClick.RemoveAllListeners();
            
            base.OnPhaseEnd(phase);
        }

        public void Apply()
        {
            current.CurrentRoomAction.AddAllValidEffect();
            current.SetResult(true);
            Debug.Log($"Apply");
        }

        public void Abort()
        {
            current.SetResult(false);
            Debug.Log($"Abort");
        }
    }
}