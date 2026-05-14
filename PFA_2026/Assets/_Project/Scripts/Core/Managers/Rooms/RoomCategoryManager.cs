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
        
        public static Npc[] SetDefaultCurrentNpcs(this Category category)
        {
            var obligateNpcs = category.ObligateNpcs;
            for (int i = 0; i < obligateNpcs.Length; i++)
                obligateNpcs[i]?.SetCategory(category);
            
            var currentNpc = obligateNpcs.Length == 0 ? new Npc[category.Quantity] : obligateNpcs;
            return currentNpc;
        }
        
        public static bool TryAddNpc(this Category category, Npc npc)
        {
            if (npc.CurrentCategory != category)
                npc.CurrentCategory?.RemoveNpc(npc);
            
            if (category.CurrentNpcs.Contains(npc))
                category.RemoveNpc(npc);
            
            for (int i = 0; i < category.CurrentNpcs.Length; i++)
            {
                if (category.CurrentNpcs[i] != null)
                    continue;
                
                category.CurrentNpcs[i] = npc;
                OnNpcAdded?.Invoke(category);
                return true;
            }
            return false;
        }

        public static void RemoveNpc(this Category category, Npc npc)
        {
            for (int i = 0; i < category.CurrentNpcs.Length; i++)
            {
                if (category.CurrentNpcs[i] != npc)
                    continue;
                category.CurrentNpcs[i] = null;
            }
            OnNpcRemove?.Invoke(category);
        }
        
        public static void ClearNpc(this Category category)
        {
            for (int i = 0; i < category.CurrentNpcs.Length; i++)
                category.CurrentNpcs[i]?.SetCategory(null);
            
            category.ClearCurrentNpcs();
            category.SetDefaultCurrentNpcs();
        }
    }
}