using System.Collections.Generic;
using Naussilus.Core.VisualNovels.EventDatas;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public static class EventManager
    {
        private static readonly Dictionary<string, EventData> EventDatas;

        static EventManager()
        {
            EventDatas = new ();
            var entries = Resources.LoadAll<EventData>("ScriptableObjects/VisualNovel/Event");
            for (int i = 0; i < entries.Length; i++)
            {
                EventData entry = entries[i];
                EventDatas.Add(entry.GUID, entry);
            }
        }

        public static EventData GetValidEvent()
        {
            //change to random valid event
            foreach ((string key, EventData value) in EventDatas)
                return value; 
            return null;
        }
        

        public static EventData TryGetEvent(string guid)
        {
            EventDatas.TryGetValue(guid, out EventData value);
            return value;
        }
    }
}