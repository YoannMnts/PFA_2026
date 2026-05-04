using System.Collections.Generic;
using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers.Npcs;
using UnityEngine;

namespace Rooms
{
    public class SelectNpcForCategory : PhaseCompletionSource<bool>
    {
        public Category CurrentCategory { get; private set; }
        public Npc[] ProhibitedNpc => CurrentCategory.ProhibitedNpcs;
        
        public int Index { get; private set; }
        public List<Npc> Npcs { get; private set; } = new List<Npc>();
        public SelectNpcForCategory(Category category, int ind)
        {
            CurrentCategory = category;
            Index = ind;
        }

        protected override Awaitable Initialize(CancellationToken token)
        {
            NpcManager.GetAllNpcs(out var allNpcs);
            Npcs.AddRange(allNpcs);
            for (int i = 0; i < ProhibitedNpc.Length; i++)
                Npcs.Remove(ProhibitedNpc[i]);

            for (int i = 0; i < Npcs.Count; i++)
                if (Npcs[i].CategoryLocked)
                    Npcs.RemoveAt(i);
            
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            Npcs = null;
            return base.Dispose(token);
        }
    }
}