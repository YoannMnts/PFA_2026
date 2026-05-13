using System;
using System.Collections.Generic;
using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using Naussilus.Gameplay.Management.Phases;
using UnityEngine;

public class ManagementPhase : PhaseCompletionSource<bool>
{
    public static void AddNpcClickListener(INpcClickListener npcClickListener) => NpcClickListeners.Add(npcClickListener);
    public static void RemoveNpcClickListener(INpcClickListener npcClickListener) => NpcClickListeners.Remove(npcClickListener);

    private static readonly List<INpcClickListener> NpcClickListeners = new List<INpcClickListener>();
    public ActionPoint CurrentActionPoint { get; private set; }
    public Npc[] CurrentNpcs { get; private set; }
    
    private readonly int defaultAP;
    public ManagementPhase(int defaultAPValue)
    {
        defaultAP = defaultAPValue;
        CurrentNpcs = NpcManager.GetAllNpcs();
    }
    
    protected override Awaitable Initialize(CancellationToken token)
    {
        CurrentActionPoint = new ActionPoint(defaultAP);
        RoomManager.SubtractAllCountdown();
        return base.Initialize(token);
    }

    protected override Awaitable Dispose(CancellationToken token)
    {
        ConditionalEffectManager.ComputeScheduledEffects();
        return base.Dispose(token);
    }

    public void NpcClicked(Npc npc)
    {
        NpcClickListeners.Sort();
        for (int i = 0; i < NpcClickListeners.Count; i++)
        {
            if (NpcClickListeners[0]?.NpcClickPriority > NpcClickListeners[i]?.NpcClickPriority)
                continue;
            
            NpcClickListeners[i]?.OnNpcClick(npc);
            Debug.Log($"Npc {npc.Name} clicked and Listener is {NpcClickListeners[i]}");
        }
        Debug.Log($"NpcLClickListeners: {NpcClickListeners.Count}");
    }
}