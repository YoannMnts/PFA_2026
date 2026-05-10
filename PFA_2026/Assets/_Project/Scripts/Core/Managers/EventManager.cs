using System;
using System.Collections.Generic;
using Naussilus.Core.VisualNovels.EventDatas;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Naussilus.Core.Managers
{
    public static class EventManager
    {
        private static readonly Dictionary<string, EventData> EventDatas;
        private static readonly Dictionary<string, Incident> Incidents;
        private static readonly List<Incident> CompletedIncidents;

        static EventManager()
        {
            EventDatas = new ();
            Incidents = new ();
            CompletedIncidents = new ();
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


        public static Incident[] GetValidEvents()
        {
            using (ListPool<Incident>.Get(out var validEventDatas) )
            {
                var isConditionValid = false;
                foreach ((string key, Incident value) in Incidents)
                {
                    if (CompletedIncidents.Contains(value))
                    {
                        Debug.Log($"[EventManager] Event already completed: {value.Name}");
                        continue;
                    }
                    
                    Debug.Log($"[EventManager] Found {key} : Incident: {value.Name}");
                    ConditionalEffect[] conditionalEffects = value.Dependencies ?? Array.Empty<ConditionalEffect>();
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
                    return null;
                }
                
                validEventDatas.Sort();
                validEventDatas.Reverse();
                var maxPriority = validEventDatas[0].Priority;
                var valueCount = validEventDatas.Count;
                
                using (ListPool<Incident>.Get(out var incidents))
                {
                    for (int i = 0; i < valueCount; i++)
                    {
                        if (validEventDatas[i].Priority == maxPriority)
                            incidents.Add(validEventDatas[i]);
                    }
                    
                    Incident[] result = new Incident[3];
                    for (int i = 0; i < result.Length; i++)
                    {
                        var randomIndex = Random.Range(0, incidents.Count);
                        result[i] = incidents[randomIndex];
                    }
                    Debug.Log($"[Event Manager] Found {validEventDatas.Count} valid events and {result[0].Name}, {result[1].Name}, {result[2].Name} has been take.");
                    return result;
                }
            }
        }

        public static void AddToCompletedEvent(this Incident incident)
        {
            CompletedIncidents.Add(incident);
        }
        
        public static Incident TryGetEvent(string guid)
        {
            Incidents.TryGetValue(guid, out Incident value);
            return value;
        }
    }
}