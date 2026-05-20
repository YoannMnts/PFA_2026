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
        
        public int Quantity { get; private set; }
        
        public Npc[] ProhibitedNpcs { get; private set; }
        
        public Npc[] ObligateNpcs { get; private set; }

        public CategoryNpcSlot[] RoomNpcSlots { get; private set; }
        

        public Category(CategoryData data)
        {
            Name = data.Name;
            Quantity = data.Quantity;
            ProhibitedNpcs = data.ProhibitedNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            ObligateNpcs = data.ObligateNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            RoomNpcSlots = data.SlotPositions?.Select(s => new CategoryNpcSlot(s.Position)).ToArray();
        }

        public void TryAddNpc(Npc npc)
        {
            for (int i = 0; i < RoomNpcSlots.Length; i++)
            {
                var roomNpcSlot = RoomNpcSlots[i];
                if (!roomNpcSlot.TryAddNpc(npc)) 
                    continue;
                OnCategoryChanged?.Invoke(this);
                return;
            }
        }

        public void TryRemoveNpc(Npc npc)
        {
            for (int i = 0; i < RoomNpcSlots.Length; i++)
            {
                var roomNpcSlot = RoomNpcSlots[i];
                if (!roomNpcSlot.TryRemoveNpc(npc)) 
                    continue;
                OnCategoryChanged?.Invoke(this);
                return;
            }
        }
        
        public void ClearAllSlots()
        {
            for (int i = 0; i < RoomNpcSlots.Length; i++)
            {
                RoomNpcSlots[i].ClearNpc();
            }
        }
    }
}