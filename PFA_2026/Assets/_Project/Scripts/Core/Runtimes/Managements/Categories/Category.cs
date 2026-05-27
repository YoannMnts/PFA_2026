using System;
using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using Naussilus.Gameplay;
using UnityEngine;

namespace Naussilus.Core
{
    public class Category
    {
        public event Action<Category> OnCategoryChanged;
        
        public string Name { get; private set; }
        
        public Npc[] ProhibitedNpcs { get; private set; }
        
        public Npc[] ObligateNpcs { get; private set; }

        public CategoryNpcSlot[] CategoryNpcSlots { get; private set; }
        

        public Category(CategoryData data)
        {
            Name = data.Name;
            ProhibitedNpcs = data.ProhibitedNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            ObligateNpcs = data.ObligateNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            CategoryNpcSlots = data.SlotPositions?.Select(s => new CategoryNpcSlot(s)).ToArray();
            for (int i = 0; i < ObligateNpcs?.Length; i++)
            {
                CategoryNpcSlots?[i].TryAddNpc(ObligateNpcs[i]);
            }
        }

        public void TryAddNpc(Npc npc)
        {
            if (ProhibitedNpcs.Contains(npc))
                return;

            for (int i = 0; i < CategoryNpcSlots.Length; i++)
            {
                var categoryNpcSlot = CategoryNpcSlots[i];
                if (categoryNpcSlot.CurrentNpc == npc)
                {
                    return;
                }
            }
            
            for (int i = 0; i < CategoryNpcSlots.Length; i++)
            {
                var roomNpcSlot = CategoryNpcSlots[i];
                if (!roomNpcSlot.TryAddNpc(npc)) 
                    continue;
                OnCategoryChanged?.Invoke(this);
                return;
            }
        }

        public void TryRemoveNpc(Npc npc)
        {
            if (ObligateNpcs.Contains(npc))
            {
                return;
            }
            
            for (int i = 0; i < CategoryNpcSlots.Length; i++)
            {
                var roomNpcSlot = CategoryNpcSlots[i];
                if (!roomNpcSlot.TryRemoveNpc(npc)) 
                    continue;
                OnCategoryChanged?.Invoke(this);
                return;
            }
        }
        
        public void ClearAllSlots()
        {
            for (int i = 0; i < CategoryNpcSlots.Length; i++)
            {
                CategoryNpcSlots[i].ClearNpc();
            }
        }
    }
}