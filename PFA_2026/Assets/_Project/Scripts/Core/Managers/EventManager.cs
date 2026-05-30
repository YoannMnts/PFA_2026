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
            CompletedIncidents = new ();
            Incidents = new ();
            var entries = Resources.LoadAll<EventData>("ScriptableObjects/VisualNovel/Event");
            EventDatas.Clear();
            for (int i = 0; i < entries.Length; i++)
            {
                EventData entry = entries[i];
                EventDatas.TryAdd(entry.GUID, entry);
            }
            
            Debug.Log($"[EventManager] Loaded {entries.Length} events.");
        }

        public static void Init()
        {
            CompletedIncidents.Clear();
        }
        
        public static Incident[] GetValidEvents(int currentDay)
        {
            using (ListPool<Incident>.Get(out var validEventDatas) )
            {
                var isConditionValid = false;
                foreach ((string guid, EventData data) in EventDatas)
                {
                    Incident currentIncident;
                    if(Incidents.TryGetValue(guid, out Incident incident))
                        currentIncident = incident;
                    else
                    {
                        currentIncident = new Incident(data);
                        Incidents.Add(guid, currentIncident);
                    }
                    
                    if (CompletedIncidents.Contains(currentIncident))
                    {
                        Debug.Log($"[EventManager] Event already completed: {currentIncident.Name}");
                        continue;
                    }
                    
                    Debug.Log($"[EventManager] Found Incident: {currentIncident.Name}");
                    ConditionalEffect[] conditionalEffects = currentIncident.Dependencies ?? Array.Empty<ConditionalEffect>();
                    for (int i = 0; i < conditionalEffects.Length; i++)
                    {
                        isConditionValid = conditionalEffects[i].ComputeOnlyConditions(currentIncident.Npcs[0]);
                        if (!isConditionValid) 
                            break;
                    }
                    if (!isConditionValid && conditionalEffects.Length > 0)
                        continue;
                    if (currentDay < currentIncident.DayCheck)
                        continue;
                    
                    validEventDatas.Add(currentIncident);
                }

                if (validEventDatas.Count == 0)
                {
                    Debug.LogError("[Event Manager] No valid events found");
                    return null;
                }
                
                validEventDatas.Sort();
                validEventDatas.Reverse();
                
                var maxPriority = validEventDatas[0].Priority;
                validEventDatas.RemoveAll(incident => incident.Priority < maxPriority);
                
                var length = Mathf.Min(3, validEventDatas.Count);
                var incidents = new Incident[length];
                for (int i = 0; i < length; i++)
                {
                    var randomIndex = Random.Range(0, validEventDatas.Count);
                    incidents[i] = validEventDatas[randomIndex];

                    if (length <= 1)
                        break;    
                    
                    Npc npc = validEventDatas[randomIndex].Npcs[0];
                    validEventDatas.RemoveAll(incident => incident.Npcs[0] == npc);
                }

                for (int i = 0; i < incidents.Length; i++)
                {
                    Debug.Log($"[Event Manager] Found {incidents.Length} valid events and {incidents[i].Name} has been take.");
                }
                
                return incidents;
            }
        }

        public static void AddToCompletedEvent(this Incident incident)
        {
            CompletedIncidents.Add(incident);
        }
    }
}