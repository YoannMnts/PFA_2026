using System.Collections.Generic;
using Naussilus.Gameplay;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class DefaultSlotManager
    {
        private static Dictionary<Npc, List<CategoryNpcSlot>> defaultSlots = new Dictionary<Npc, List<CategoryNpcSlot>>();

        public static void Register(this CategoryNpcSlot slot)
        { 
            for (int i = 0; i < slot.DefaultNpcs.Length; i++)
            {
                var defaultNpc = slot.DefaultNpcs[i];
                if (!defaultSlots.ContainsKey(defaultNpc))
                {
                    defaultSlots.Add(defaultNpc, new List<CategoryNpcSlot>());
                }

                defaultSlots[defaultNpc].Add(slot);
            }
        }

        public static void Unregister(this CategoryNpcSlot slot)
        {
            for (int i = 0; i < slot.DefaultNpcs.Length; i++)
            {
                var defaultNpc = slot.DefaultNpcs[i];
                if (!defaultSlots.ContainsKey(defaultNpc))
                {
                    return;
                }

                defaultSlots[defaultNpc].Remove(slot);
            }
        }

        public static void AddToRandomSlot(this Npc npc)
        {
            if (!defaultSlots.TryGetValue(npc, out var list)) 
                return;
            
            while (true)
            {
                var randomNumber = Random.Range(0, list.Count);
                if (!list[randomNumber].TryAddNpc(npc)) 
                    return;
                break;
            }
        }
    }
}