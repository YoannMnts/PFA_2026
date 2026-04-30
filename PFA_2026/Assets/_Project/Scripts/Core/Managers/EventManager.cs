using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Conditions;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.VisualNovels.EventDatas;
using UnityEngine;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers
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
            using (ListPool<EventData>.Get(out var validEventDatas) )
            {
                var isConditionValid = false;
                foreach ((string key, EventData value) in EventDatas)
                {
                    ConditionalEffect[] conditionalEffects = value.Dependencies;
                    for (int i = 0; i < conditionalEffects.Length; i++)
                    {
                        isConditionValid = conditionalEffects[i].Conditions.ComputeAllCondition(value.Npcs[0]);
                        if (!isConditionValid) 
                            break;
                    }
                    if (!isConditionValid)
                        continue;
                    
                    validEventDatas.Add(value);
                }

                if (validEventDatas.Count == 0)
                {
                    Debug.LogError("[Event Manager] No valid events found");
                    return null;
                }
                
                var randomIndex = Random.Range(0, validEventDatas.Count);
                return validEventDatas[randomIndex];
            }
        }
        
        public static EventData TryGetEvent(string guid)
        {
            EventDatas.TryGetValue(guid, out EventData value);
            return value;
        }
    }
}