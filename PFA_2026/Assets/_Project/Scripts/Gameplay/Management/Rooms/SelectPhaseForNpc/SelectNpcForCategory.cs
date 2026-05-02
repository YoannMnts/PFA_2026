using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers.Npcs;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Rooms
{
    public class SelectNpcForCategory : PhaseCompletionSource<bool>
    {
        public Category CurrentCategory { get; private set; }
        public Npc[] ProhibitedNpc => CurrentCategory.ProhibitedNpcs;
        
        public int Index { get; private set; }
        public Npc[] Npcs { get; private set; }
        public SelectNpcForCategory(Category category, int ind)
        {
            CurrentCategory = category;
            Index = ind;
        }

        protected override Awaitable Initialize(CancellationToken token)
        {
            using (ListPool<Npc>.Get(out var list))
            {
                NpcManager.GetAllNpcs(out var allNpcs);
                list.AddRange(allNpcs);
                for (int i = 0; i < ProhibitedNpc.Length; i++)
                {
                    list.Remove(ProhibitedNpc[i]);
                }
                Npcs = list.ToArray();
            }
            
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            Npcs = null;
            return base.Dispose(token);
        }
    }
}