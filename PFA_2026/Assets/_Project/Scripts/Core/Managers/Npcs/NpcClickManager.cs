using System.Collections.Generic;
using Naussilus.Gameplay.Management.Phases;
using UnityEngine;

namespace Naussilus.Core.Managers.Npcs
{
    public static class NpcClickManager
    {
        public static void AddNpcClickListener(this INpcClickListener npcClickListener) => NpcClickListeners.Add(npcClickListener);
        public static void RemoveNpcClickListener(this INpcClickListener npcClickListener) => NpcClickListeners.Remove(npcClickListener);
        
        private static readonly List<INpcClickListener> NpcClickListeners = new List<INpcClickListener>();
        
        public static void NpcClicked(this Npc npc)
        {
            NpcClickListeners.Sort();
            for (int i = 0; i < NpcClickListeners.Count; i++)
            {
                if (NpcClickListeners[0]?.NpcClickPriority > NpcClickListeners[i]?.NpcClickPriority)
                    continue;
            
                NpcClickListeners[i]?.OnNpcClick(npc);
                Debug.Log($"Npc {npc.Name} clicked and Listener is {NpcClickListeners[i]}");
            }
            Debug.Log($"NpcLClickListeners: {NpcClickListeners.Count}");
        }
    }
}