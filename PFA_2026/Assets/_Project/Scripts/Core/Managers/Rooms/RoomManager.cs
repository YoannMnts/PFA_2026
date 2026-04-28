using System.Collections.Generic;
using Naussilus.Core.Managements.RoomDatas;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Managers.Rooms
{
    public static class RoomManager
    {
        private static readonly Dictionary<string, RoomData> RoomDatas;
        private static readonly RoomData[] Entries;
        
        static RoomManager()
        {
            RoomDatas = new ();
            var entries = Resources.LoadAll<RoomData>("ScriptableObjects/Management/Room");
            for (int i = 0; i < entries.Length; i++)
            {
                RoomData entry = entries[i];
                RoomDatas.Add(entry.GUID, entry);
            }
            
            Entries = entries;
        }
        
        public static RoomData[] GetAllRooms()
        {
            return Entries;
        }
    }
}