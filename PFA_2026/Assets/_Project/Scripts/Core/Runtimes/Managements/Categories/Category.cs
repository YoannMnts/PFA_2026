using System;
using System.Linq;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;

namespace Naussilus.Core
{
    public struct Category
    {
        public event Action<Category> OnNpcAdded;
        
        public string Name { get; private set; }
        
        public int Quantity { get; private set; }
        
        public Npc[] ProhibitedNpcs { get; private set; }
        
        public Npc[] ObligateNpcs { get; private set; }
        
        public Npc[] CurrentNpcs { get; private set; }

        public Category(CategoryData data)
        {
            Name = data.Name;
            Quantity = data.Quantity;
            ProhibitedNpcs = data.ProhibitedNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            ObligateNpcs = data.ObligateNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            CurrentNpcs = ObligateNpcs ?? new Npc[Quantity];
            OnNpcAdded = null;
        }

        public void AddNpc(Npc npc, int index)
        {
            CurrentNpcs[index] = npc;
            OnNpcAdded?.Invoke(this);
        }
    }
}