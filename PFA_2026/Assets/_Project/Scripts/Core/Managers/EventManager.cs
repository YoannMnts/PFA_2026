using System.Collections.Generic;
using Naussilus.Core.VisualNovels.EventDatas;
using UnityEngine;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers
{
    public static class EventManager
    {
        private static readonly Dictionary<string, EventData> EventDatas;
        private static readonly Dictionary<string, Incident> Incidents;

        static EventManager()
        {
            EventDatas = new ();
            Incidents = new ();
            var entries = Resources.LoadAll<EventData>("ScriptableObjects/VisualNovel/Event");
            for (int i = 0; i < entries.Length; i++)
            {
                EventData entry = entries[i];
                EventDatas.Add(entry.GUID, entry);
            }

            for (int i = 0; i < entries.Length; i++)
            {
                Incident entry = new Incident(entries[i]);
                Incidents.Add(entries[i].GUID, entry);
            }
            Debug.Log($"[EventManager] Loaded {entries.Length} events.");
        }
        
        public static void Init(){}


        public static Incident GetValidEvent()
        {
            using (ListPool<Incident>.Get(out var validEventDatas) )
            {
                var isConditionValid = false;
                foreach ((string key, Incident value) in Incidents)
                {
                    ConditionalEffect[] conditionalEffects = value.Dependencies;
                    for (int i = 0; i < conditionalEffects.Length; i++)
                    {
                        isConditionValid = conditionalEffects[i].ComputeOnlyConditions(value.Npcs[0]);
                        if (!isConditionValid) 
                            break;
                    }
                    if (!isConditionValid && conditionalEffects.Length > 0)
                        continue;
                    
                    validEventDatas.Add(value);
                }

                if (validEventDatas.Count == 0)
                {
                    Debug.LogError("[Event Manager] No valid events found");
                    return new Incident();
                }
                
                var randomIndex = Random.Range(0, validEventDatas.Count);
                return validEventDatas[randomIndex];
            }
        }
        
        public static Incident TryGetEvent(string guid)
        {
            Incidents.TryGetValue(guid, out Incident value);
            return value;
        }
    }
}