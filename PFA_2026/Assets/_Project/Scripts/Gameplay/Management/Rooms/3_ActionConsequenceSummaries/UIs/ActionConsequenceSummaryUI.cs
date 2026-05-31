using System.Collections.Generic;
using System.Linq;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class ActionConsequenceSummaryUI : MonoPhaseListener<ActionConsequenceSummary>
    {
        private ActionConsequenceSummary current;

        [SerializeField] private CanvasGroup group;

        [SerializeField] private ActionConsequenceUIList actionConsequenceUIList;
        
        [SerializeField] private Button applyButton;
        
        [SerializeField] private CanvasGroup npcBarGroup;
        
        

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
            npcBarGroup.Hide();
            var validConsequence = ConditionalEffectManager.ValidConsequences;
            using (ListPool<Consequence>.Get(out var list))
            {
                for (int i = 0; i < validConsequence.Count; i++)
                {
                    var consequence = validConsequence[i];
                    if (!consequence.Text.All(string.IsNullOrEmpty))
                        list.Add(consequence);
                    
                    actionConsequenceUIList.Connect(list);
                }
            }    
            applyButton.onClick.AddListener(Apply);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ActionConsequenceSummary phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            group.Hide();
            npcBarGroup.Show();
            applyButton.onClick.RemoveAllListeners();
            
            base.OnPhaseEnd(phase);
        }

        public void Apply()
        {
            current.SetResult(true);
            Debug.Log($"Apply");
        }

        public void Abort()
        {
            if (current != null)
                current.SetResult(false);
            Debug.Log($"Abort");
        }
    }
}