using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Rooms
{
    public class SelectNpcForCategory : PhaseCompletionSource<NpcData>
    {
        public CategoryData CurrentCategory { get; private set; }
        public NpcData[] ObligateNpc => CurrentCategory.ObligateNpc;
        public NpcData[] ProhibitedNpc => CurrentCategory.ProhibitedNpc;
        
        public NpcData[] Npcs { get; private set; }
        public SelectNpcForCategory(CategoryData category)
        {
            CurrentCategory = category;
        }

        protected override Awaitable Initialize(CancellationToken token)
        {
            if (ObligateNpc.Length > 0)
            {
                Npcs = ObligateNpc;
            }
            else
            {
                using (ListPool<NpcData>.Get(out var list))
                {
                    NpcManager.GetAllNpcs(out var allNpcs);
                    list.AddRange(allNpcs);
                    for (int i = 0; i < ProhibitedNpc.Length; i++)
                    {
                        list.Remove(ProhibitedNpc[i]);
                    }
                    Npcs = list.ToArray();
                }
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