using System.Collections.Generic;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Gameplay.Npcs
{
    public static class NpcManager
    {
        private static readonly Dictionary<string, NpcData> NpcDatas;
        private static readonly Dictionary<string, Npc> Npcs;
        
        static NpcManager()
        {
            NpcDatas = new ();
            Npcs = new ();
            var entries = Resources.LoadAll<NpcData>("ScriptableObjects/Npc");
            for (int i = 0; i < entries.Length; i++)
            {
                NpcData entry = entries[i];
                NpcDatas.Add(entry.GUID, entry);

                var npc = new Npc(entry);
                Npcs.Add(entry.GUID, npc);
            }
        }
        
        public static NpcData TryGetData(string guid)
        {
            NpcDatas.TryGetValue(guid, out NpcData value);
            return value;
        }
        
        public static NpcData TryGetData(string guid, out NpcData data)
        {
            NpcDatas.TryGetValue(guid, out NpcData value);
            return data = value;
        }
        
        public static Npc TryGetNpc(string guid, out Npc npc)
        {
            Npcs.TryGetValue(guid, out Npc value);
            return npc = value;
        }

        public static Npc TryGetNpc(string guid)
        {
            Npcs.TryGetValue(guid, out Npc value);
            return value;
        }
    }
}