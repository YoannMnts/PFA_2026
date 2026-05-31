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
                ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(this.gameObject);
                await Awaitable.WaitForSecondsAsync(0.3f);
            }
            currentPhase.SetResult(true);
        }
    }
}