using Naussilus.Gameplay;

public class EndDayButton : PhaseButton<ManagementPhase>
{
    protected override void OnButtonClicked()
    {
        currentPhase.SetResult(true);
    }
}