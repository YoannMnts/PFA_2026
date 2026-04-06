namespace Naussilus.Gameplay.Scripts.UIs
{
    public class DecisionButton : PhaseButton<Decision>
    {
        protected override void OnButtonClicked()
        {
            currentPhase.EndPhase(true);
        }
    }
}