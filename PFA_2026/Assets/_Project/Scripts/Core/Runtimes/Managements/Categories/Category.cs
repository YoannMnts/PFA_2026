using System;
using System.Linq;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core
{
    public class Category
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
            CurrentNpcs = ObligateNpcs == null || ObligateNpcs.Length == 0 ? new Npc[Quantity] : ObligateNpcs;
            OnNpcAdded = null;
        }

        public void AddNpc(Npc npc, int index)
        {
            var ind = Mathf.Clamp(index, 0, CurrentNpcs.Length);
            CurrentNpcs[ind] = npc;
            OnNpcAdded?.Invoke(this);
        }
    }
}