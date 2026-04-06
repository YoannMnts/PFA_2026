namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class SummaryButton : PhaseButton<SummaryWait>
    {
        protected override void OnButtonClicked()
        {
            currentPhase.SetResult(true);
        }
    }
}