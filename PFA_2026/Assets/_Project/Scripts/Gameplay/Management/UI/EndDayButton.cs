namespace Naussilus.Gameplay.Management._Project.Scripts
{
    public class EndDayButton : PhaseButton<ManagementPhase>
    {
        protected override void OnButtonClicked()
        {
            currentPhase.EndPhase(true);
        }
    }
}