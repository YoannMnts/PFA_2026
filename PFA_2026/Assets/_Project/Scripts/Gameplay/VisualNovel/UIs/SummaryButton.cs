namespace Naussilus.Gameplay.VisualNovel
{
    public class SummaryButton : PhaseButton<SummaryWait>
    {
        protected override void OnButtonClicked()
        {
            currentPhase.SetResult(true);
        }
    }
}