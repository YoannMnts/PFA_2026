using System.Collections.Generic;
using System.Linq;
using Naussilus.Gameplay;
using UnityEngine;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers
{
    public static class DefaultSlotManager
    {
        private static List<CategoryNpcSlot> defaultSlots = new List<CategoryNpcSlot>();

        public static void Register(this CategoryNpcSlot slot)
        {
            if (defaultSlots.Contains(slot))
            {
                return;
            }
            defaultSlots.Add(slot);
            
        }

        public static void Unregister(this CategoryNpcSlot slot)
        {
            defaultSlots.Remove(slot);
        }

        public static void AddToRandomSlot(this Npc npc)
        {
            using (ListPool<CategoryNpcSlot>.Get(out var list))
            {
                for (int i = 0; i < defaultSlots.Count; i++)
                {
                    var slot = defaultSlots[i];
                    if (slot.ValidNpcsToDefault.Contains(npc))
                    {
                        list.Add(slot);
                    }
                }
                
                npc.SetInRandomSlot(list);
            }
        }

        private static void SetInRandomSlot(this Npc npc, List<CategoryNpcSlot> slots)
        {
            while (slots.Count > 0)
            {
                var randomNumber = Random.Range(0, slots.Count);
                if (!slots[randomNumber].TryAddDefaultNpc(npc))
                {
                    slots.Remove(slots[randomNumber]);
                    continue;
                }

                break;
            }

            if (slots.Count == 0)
                Debug.LogWarning($"[DefaultSlotManager] No available slot for {npc.Name}");
        }
    }
}