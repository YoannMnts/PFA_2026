using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers.Npcs;
using UnityEngine;

public class CheckNpcState : PhaseCompletionSource<bool>
{
    public ManagementNpc CurrentManagementNpc { get; private set; }
    public Npc CurrentNpc => NpcManager.TryGetNpc(CurrentManagementNpc.NpcData.GUID);
        
    public Behavior[] NpcBehaviors { get; private set; }
    public MentalState[] NpcMentalStates { get; private set; }
        
    public CheckNpcState(ManagementNpc managementNpc)
    {
        CurrentManagementNpc = managementNpc;
    }
        
    protected override Awaitable Initialize(CancellationToken token)
    {
        NpcBehaviors = CurrentNpc.Behaviors;
        NpcMentalStates = CurrentNpc.MentalStates;
        return base.Initialize(token);
    }

    protected override Awaitable Dispose(CancellationToken token)
    {
        NpcBehaviors = null;
        NpcMentalStates = null;
        return base.Dispose(token);
    }
}