using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel.VisualNovelSummaries.UIs
{
    public class VisualNovelSummaryUI : MonoPhaseListener<VisualNovelSummary>
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private ConsequenceSummaryUIList consequenceSummaryUIList;

        private VisualNovelSummary currentPhase;
        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(VisualNovelSummary phase)
        {
            if (currentPhase != null)
                return;
                
            currentPhase = phase;

            if (phase.CurrentConsequences.Count <= 0)
            {
                currentPhase.SetResult(true);
                return;
            }
            
            group.Show();
            consequenceSummaryUIList.Connect(phase.CurrentConsequences);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(VisualNovelSummary phase)
        {
            if (currentPhase == null)
                return;
            
            currentPhase = null;
            group.Hide();
            consequenceSummaryUIList.Disconnect();
            
            base.OnPhaseEnd(phase);
        }

        public void OnButtonClicked()
        {
            currentPhase.SetResult(true);
        }
    }
}