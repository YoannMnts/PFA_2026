using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class CheckNpcState : PhaseCompletionSource<bool>
    {
        public MonoNpc CurrentMonoNpc { get; private set; }
        public Npc CurrentNpc => CurrentMonoNpc.Npc;
        
        public Behavior[] NpcBehaviors { get; private set; }
        public MentalState[] NpcMentalStates { get; private set; }
    
        public NpcRelationship[] NpcRelationships { get; private set; }
        
        public CheckNpcState(MonoNpc monoNpc)
        {
            CurrentMonoNpc = monoNpc;
        }
        
        protected override Awaitable Initialize(CancellationToken token)
        {
            NpcBehaviors = CurrentNpc.Behaviors;
            NpcMentalStates = CurrentNpc.MentalStates;
            NpcRelationships = CurrentNpc.Relationships;
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            NpcBehaviors = null;
            NpcMentalStates = null;
            return base.Dispose(token);
        }
    }
}