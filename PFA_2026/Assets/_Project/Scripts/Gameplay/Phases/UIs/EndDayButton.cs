namespace Naussilus.Gameplay.Scripts.UIs
{
    public class EndDayButton : PhaseButton<ManagementPhase>
    {
        protected override void OnButtonClicked()
        {
            currentPhase.EndPhase(true);
        }
    }
}