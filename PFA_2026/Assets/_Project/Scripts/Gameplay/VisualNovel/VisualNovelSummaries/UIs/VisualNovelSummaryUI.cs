using System.Collections.Generic;
using System.Linq;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Sounds;
using Naussilus.Gameplay.Buttons;
using UnityEngine;
using UnityEngine.Pool;

namespace Naussilus.Gameplay
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
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.PlaySound(SoundsEnum.Writing);
            }
            using (ListPool<Consequence>.Get(out var list))
            {
                for (int i = 0; i < phase.CurrentConsequences.Count; i++)
                {
                    var consequence = phase.CurrentConsequences[i];
                    if (!consequence.Text.All(string.IsNullOrEmpty))
                        list.Add(consequence);
                    
                    consequenceSummaryUIList.Connect(list);
                }
            }
            
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

        public async void OnButtonClicked(GameObject button)
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(button);
            }
            currentPhase.SetResult(true);
        }
    }
}