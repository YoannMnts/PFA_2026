using System.Collections.Generic;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.VisualNovels.EventDatas;
using UnityEngine;

namespace Naussilus.Gameplay.Npcs
{
    public static class NpcManager
    {
        private static readonly Dictionary<string, NpcData> NpcDatas;

        static NpcManager()
        {
            NpcDatas = new ();
            var entries = Resources.LoadAll<NpcData>("ScriptableObjects/Npc");
            for (int i = 0; i < entries.Length; i++)
            {
                NpcData entry = entries[i];
                NpcDatas.Add(entry.GUID, entry);
            }
        }
        
        public static NpcData TryGetNpc(string guid)
        {
            NpcDatas.TryGetValue(guid, out NpcData value);
            return value;
        }
    }
}