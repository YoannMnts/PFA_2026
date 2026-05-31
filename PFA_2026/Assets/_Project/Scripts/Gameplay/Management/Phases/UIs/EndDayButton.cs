using Naussilus.Gameplay.Buttons;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class EndDayButton : PhaseButton<ManagementPhase>
    {
        protected async override void OnButtonClicked()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(this.gameObject);
            }
            currentPhase.SetResult(true);
        }
    }
}