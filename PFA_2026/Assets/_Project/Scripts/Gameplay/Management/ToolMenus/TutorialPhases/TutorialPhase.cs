using System.Threading;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class TutorialPhase : PhaseCompletionSource<bool>
    {
        protected override Awaitable Initialize(CancellationToken token)
        {
            TimerPhase.SetTimerPause(true);
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            TimerPhase.SetTimerPause(false);
            return base.Dispose(token);
        }
    }
}