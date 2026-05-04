using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naussilus.Core.Managers.Rooms
{
    public static class RoomCategoryManager
    {
        public static event Action<Category> OnNpcAdded;
        public static event Action<Category> OnNpcRemove;
        
        public static List<Npc> SetDefaultCurrentNpcs(this Category category)
        {
            var obligateNpcs = category.ObligateNpcs;
            for (int i = 0; i < obligateNpcs.Length; i++)
                obligateNpcs[i]?.SetCategory(category, true);
            
            var currentNpc = obligateNpcs.Length == 0 ? new Npc[category.Quantity] : obligateNpcs;
            var list = new List<Npc>();
            list.AddRange(currentNpc);
            return list;
        }
        
        public static void AddNpc(this Category category, Npc npc, int index)
        {
            if (npc.CurrentCategory != category)
                npc.CurrentCategory?.RemoveNpc(npc);
            
            if (category.CurrentNpcs.Contains(npc))
                category.RemoveNpc(npc);
            
            var ind = Mathf.Clamp(index, 0, category.CurrentNpcs.Count);
            Debug.Log(ind);
            category.CurrentNpcs[ind] = npc;
            npc.SetCategory(category, false);
            OnNpcAdded?.Invoke(category);
        }

        public static void RemoveNpc(this Category category, Npc npc)
        {
            for (int i = 0; i < category.CurrentNpcs.Count; i++)
            {
                if (category.CurrentNpcs[i] != npc)
                    continue;
                category.CurrentNpcs[i] = null;
            }
            OnNpcRemove?.Invoke(category);
        }
        
        public static void ClearNpc(this Category category)
        {
            for (int i = 0; i < category.CurrentNpcs.Count; i++)
                category.CurrentNpcs[i]?.SetCategory(null, false);
            
            category.CurrentNpcs.Clear();
            category.CurrentNpcs.AddRange(category.SetDefaultCurrentNpcs());
        }
    }
}