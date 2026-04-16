using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace _Project.Scripts
{
    public class CheckNpcState : PhaseCompletionSource<bool>
    {
        public Npc CurrentNpc { get; private set; }
        public NpcData CurrentNpcData => CurrentNpc.NpcData;
        
        public NpcBehavior[] NpcBehaviors { get; private set; }
        public NpcMentalState[] NpcMentalStates { get; private set; }
        
        public CheckNpcState(Npc npc)
        {
            CurrentNpc = npc;
        }
        
        protected override Awaitable Initialize(CancellationToken token)
        {
            NpcBehaviors = CurrentNpcData.Behavior;
            NpcMentalStates = CurrentNpcData.MentalState;
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