using System.Collections.Generic;
using System.Linq;
using Naussilus.Gameplay;
using UnityEngine;

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
            while (true)
            {
                var randomIndex = Random.Range(0, defaultSlots.Count);
                var categoryNpcSlot = defaultSlots[randomIndex];
                if(categoryNpcSlot.ValidNpcsToDefault == null)
                    continue;
                if (!categoryNpcSlot.ValidNpcsToDefault.Contains(npc))
                    continue;
                if (!categoryNpcSlot.TryAddDefaultNpc(npc))
                    continue;
                break;
            }
        }
    }
}