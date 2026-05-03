using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using UnityEngine;

public class ManagementPhase : PhaseCompletionSource<bool>
{
    protected override Awaitable Dispose(CancellationToken token)
    {
        ConditionalEffectManager.ComputeScheduledEffects();
        return base.Dispose(token);
    }
}