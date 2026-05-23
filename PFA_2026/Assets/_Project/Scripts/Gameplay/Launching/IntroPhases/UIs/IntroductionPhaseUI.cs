using Helteix.Tools.Phases.Listeners;

namespace Naussilus.Gameplay
{
    public class IntroductionPhaseUI : MonoPhaseListener<IntroductionPhase>
    {
        protected override void OnPhaseBegin(IntroductionPhase phase)
        {
            phase.SetResult(true);
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(IntroductionPhase phase)
        {
            base.OnPhaseEnd(phase);
        }
    }
}