using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Managements;
using UnityEngine;

namespace Naussilus.Core.Managers.Rooms
{
    public static class RoomManager
    {
        private static readonly Dictionary<string, RoomData> RoomDatas;
        private static readonly Dictionary<string, Room> Rooms;
        
        static RoomManager()
        {
            RoomDatas = new ();
            Rooms = new Dictionary<string, Room>();
            var entries = Resources.LoadAll<RoomData>("ScriptableObjects/Management/Room");
            for (int i = 0; i < entries.Length; i++)
            {
                RoomData entry = entries[i];
                RoomDatas.Add(entry.GUID, entry);
            }

            for (int i = 0; i < entries.Length; i++)
            {
                Room entry = new Room(entries[i]);
                Rooms.Add(entries[i].GUID, entry);
            }
        }
        
        public static void Init(){}

        
        public static Room[] GetAllRooms()
        {
            return Rooms.Values.ToArray();
        }

        public static Room TryGetRoom(string guid)
        {
            Rooms.TryGetValue(guid, out Room room);
            return room;
        }
    }
}