using System;
using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;
using UnityEngine;

namespace Naussilus.Core
{
    public class Category
    {
        public event Action<Category> OnNpcAdded;
        public event Action<Category> OnNpcClear;
        
        
        public string Name { get; private set; }
        
        public int Quantity { get; private set; }
        
        public Npc[] ProhibitedNpcs { get; private set; }
        
        public Npc[] ObligateNpcs { get; private set; }
        
        public List<Npc> CurrentNpcs { get; private set; }

        public Category(CategoryData data)
        {
            Name = data.Name;
            Quantity = data.Quantity;
            ProhibitedNpcs = data.ProhibitedNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            ObligateNpcs = data.ObligateNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            CurrentNpcs = InitCurrentNpcs();;
        }

        private List<Npc> InitCurrentNpcs()
        {
            var currentNpc = ObligateNpcs == null || ObligateNpcs.Length == 0 ? new Npc[Quantity] : ObligateNpcs;
            var list = new List<Npc>();
            list.AddRange(currentNpc);
            return list;
        }

        public void AddNpc(Npc npc, int index)
        {
            if (npc.CurrentCategory != this)
                npc.CurrentCategory?.ClearNpc();
            
            if (CurrentNpcs.Contains(npc))
                CurrentNpcs.Clear();
            
            var ind = Mathf.Clamp(index, 0, CurrentNpcs.Count);
            Debug.Log(ind);
            CurrentNpcs[ind] = npc;
            npc.SetCategory(this);
            OnNpcAdded?.Invoke(this);
        }

        public void ClearNpc()
        {
            for (int i = 0; i < CurrentNpcs.Count; i++)
                CurrentNpcs[i]?.SetCategory(null);
            
            CurrentNpcs = InitCurrentNpcs();
            OnNpcClear?.Invoke(this);
        }
    }
}