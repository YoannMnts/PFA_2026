namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class DecisionButton : PhaseButton<DecisionChoice>
    {
        protected override void OnButtonClicked()
        {
            currentPhase.SetResult(0);
        }
    }
}