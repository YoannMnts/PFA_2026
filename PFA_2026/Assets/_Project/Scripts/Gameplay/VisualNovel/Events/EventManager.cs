using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Datas.VisualNovels;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel.EventManager
{
    public static class EventManager
    {
        private static Dictionary<string, EventData> eventDatas;

        static EventManager()
        {
            eventDatas = new ();
            var entries = Resources.LoadAll<EventData>("ScriptableObjects/VisualNovel/Event");
            for (int i = 0; i < entries.Length; i++)
            {
                EventData entry = entries[i];
                eventDatas.Add(entry.GUID, entry);
            }
        }

        public static EventData GetValidEvent()
        {
            foreach ((string key, EventData value) in eventDatas)
                return value;
            return null;
        }
        

        public static EventData TryGetEvent(string guid)
        {
            eventDatas.TryGetValue(guid, out EventData value);
            return value;
        }
    }
}