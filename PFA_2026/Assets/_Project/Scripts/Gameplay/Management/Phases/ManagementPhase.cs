using System.Threading;
using DefaultNamespace;
using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using UnityEngine;

public class ManagementPhase : PhaseCompletionSource<bool>
{
    public ActionPoint CurrentActionPoint { get; private set; }

    private readonly int defaultAP;
    public ManagementPhase(int defaultAPValue)
    {
        defaultAP = defaultAPValue;
    }
    
    protected override Awaitable Initialize(CancellationToken token)
    {
        CurrentActionPoint = new ActionPoint(defaultAP);   
        return base.Initialize(token);
    }

    protected override Awaitable Dispose(CancellationToken token)
    {
        ConditionalEffectManager.ComputeScheduledEffects();
        return base.Dispose(token);
    }
}