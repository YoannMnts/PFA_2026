using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Managements;
using UnityEngine;

namespace Naussilus.Core.Managers.Rooms
{
    public static class RoomManager
    {
        private static readonly Dictionary<string, RoomData> RoomDatas;
        private static Dictionary<string, Room> rooms;
        static RoomManager()
        {
            RoomDatas = new ();
            var entries = Resources.LoadAll<RoomData>("ScriptableObjects/Management/Room");
            for (int i = 0; i < entries.Length; i++)
            {
                RoomData entry = entries[i];
                RoomDatas.Add(entry.GUID, entry);
            }

            
        }

        public static void Init()
        {
            rooms = new Dictionary<string, Room>();
            rooms.Clear();
            foreach ((string GUID, RoomData data) in RoomDatas)
            {
                var room = new Room(data);
                rooms.Add(GUID, room);
            }
        }
        
        public static Room[] GetAllRooms()
        {
            return rooms.Values.ToArray();
        }

        public static Room TryGetRoom(string guid)
        {
            rooms.TryGetValue(guid, out Room room);
            return room;
        }

        public static void SubtractAllCountdown()
        {
            foreach ((string key, Room room) in rooms)
            {
                room.AddOrRemoveCountdown(-1);
            }
        }
    }
}