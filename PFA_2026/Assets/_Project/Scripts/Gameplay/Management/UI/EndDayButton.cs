using Naussilus.Gameplay;

namespace _Project.Scripts
{
    public class EndDayButton : PhaseButton<ManagementPhase>
    {
        protected override void OnButtonClicked()
        {
            currentPhase.EndPhase(true);
        }
    }
}