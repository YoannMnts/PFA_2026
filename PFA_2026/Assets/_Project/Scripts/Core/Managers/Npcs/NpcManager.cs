using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.Conditions;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.NpcDatas;
using UnityEngine;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers.Npcs
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
            }
            
            foreach ((string key, NpcData value) in NpcDatas)
            {
                var npc = new Npc(value);
                Npcs.Add(value.GUID, npc);
            }

            foreach ((string key, NpcData value) in NpcDatas)
            {
                TryGetNpc(value.GUID, out Npc npc);
                npc.InitRelationships(value);
            }
            
            Debug.Log($"[EventManager] Loaded {entries.Length} events.");
        }
        
        public static void Init(){}
        
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
        
        public static Npc[] GetAllNpcs()
        {
            return Npcs.Values.ToArray();
        }
        
        public static void GetAllNpcs(out Npc[] allNpcs)
        {
            allNpcs = Npcs.Values.ToArray();
        }

        public static Npc[] GetSelectedNpcs(INpcSelector npcSelector , Npc currentNpc ,Category[] currentCategories)
        {
            using (ListPool<Npc>.Get(out var list))
            {
                Npc[] result = new Npc[] { };
                switch (npcSelector)
                {
                    case Npc npcValue:
                        return new[] { npcValue };
                        
                    case AllNpc :
                        var allNpcs = GetAllNpcs();
                        for (int j = 0; j < allNpcs.Length; j++)
                        {
                            if (allNpcs[j] == currentNpc)
                                continue;
                            
                            list.Add(allNpcs[j]);
                        }
                        result = list.ToArray();
                        return result;
                    
                    case Gender gender:
                        var npcDatas = GetAllNpcs();
                        for (int j = 0; j < npcDatas.Length; j++)
                        {
                            if (npcDatas[j].Gender == gender.EGender)
                            {
                                list.Add(npcDatas[j]);
                            }
                        }
                        result = list.ToArray();
                        return result;
                    
                    case CategoryIndex categoryIndex:
                        int ind = Mathf.Max(0, categoryIndex.Index - 1);
                        Category targetCategory = currentCategories[ind];
                        for (int j = 0; j < targetCategory.CurrentNpcs.Count; j++)
                        {
                            Npc categoryNpc = targetCategory.CurrentNpcs[j];
                            list.Add(categoryNpc);
                        }
                        result = list.ToArray();
                        return result;
                }
                return result;
            }
        }
        public static Npc[] GetSelectedNpcs(INpcSelector npcSelector , Npc currentNpc)
        {
            using (ListPool<Npc>.Get(out var list))
            {
                Npc[] result = new Npc[] { };
                switch (npcSelector)
                {
                    case Npc npcValue:
                        return new[] { npcValue };

                    case AllNpc:
                        var allNpcs = GetAllNpcs();
                        for (int j = 0; j < allNpcs.Length; j++)
                        {
                            if (allNpcs[j] == currentNpc)
                                continue;

                            list.Add(allNpcs[j]);
                        }

                        result = list.ToArray();
                        return result;

                    case Gender gender:
                        var npcDatas = GetAllNpcs();
                        for (int j = 0; j < npcDatas.Length; j++)
                        {
                            if (npcDatas[j].Gender == gender.EGender)
                            {
                                list.Add(npcDatas[j]);
                            }
                        }

                        result = list.ToArray();
                        return result;
                }
                return result;
            }
        }

        public static void GetSelectedNpcs(INpcSelector npcSelector, Npc currentNpc, out Npc[] npcs)
        {
            using (ListPool<Npc>.Get(out var list))
            {
                Npc[] result = new Npc[] { };
                switch (npcSelector)
                {
                    case Npc npcValue:
                        npcs = new[] { npcValue };
                        break;

                    case AllNpc:
                        var allNpcs = GetAllNpcs();
                        for (int i = 0; i < allNpcs.Length; i++)
                        {
                            if (allNpcs[i] == currentNpc)
                                continue;

                            list.Add(allNpcs[i]);
                        }

                        result = list.ToArray();
                        npcs = result;
                        break;

                    case Gender gender:
                        var npcDatas = GetAllNpcs();
                        for (int i = 0; i < npcDatas.Length; i++)
                        {
                            if (npcDatas[i].Gender == gender.EGender)
                            {
                                list.Add(npcDatas[i]);
                            }
                        }

                        result = list.ToArray();
                        npcs = result;
                        break;
                }
                npcs = result;
            }
        }
        
        public static INpcStat[] GetValue<T>(this Npc npc, T side) where T : IConditionalEffectValue
        {
            switch (side)
            {
                case IntValue intValue:
                    return new INpcStat[]{intValue};
                
                case Behavior value:
                    for (int i = 0; i < npc.Behaviors.Length; i++)
                    {
                        if (npc.Behaviors[i].Data != value.Data) 
                            continue;
                        
                        var behavior = npc.Behaviors[i];
                        return new INpcStat[]{behavior};
                    }
                    break;
                
                case MentalState value:
                    for (int i = 0; i < npc.MentalStates.Length; i++)
                    {
                        if (npc.MentalStates[i].Data != value.Data) 
                            continue;
                        
                        var mentalState = npc.MentalStates[i];
                        return new INpcStat[]{mentalState};
                    }
                    break;
                
                case NpcRelationship value:
                    GetSelectedNpcs(value.Npc, npc, out Npc[] npcs);
                    using (ListPool<INpcStat>.Get(out var list))
                    {
                        for (int i = 0; i < npc.Relationships.Length; i++)
                        {
                            for (int j = 0; j < npcs.Length; j++)
                            {
                                if (npc.Relationships[i].Npc != npcs[j])
                                    continue;
                                
                                list.Add(npc.Relationships[i]);
                            }
                        }

                        return list.ToArray();
                    }
                    
            }
            return null;
        }
        public static INpcStat[] GetValue<T>(this Npc npc, T side, out int amount) where T : IConditionalEffectValue
        {
            switch (side)
            {
                case IntValue intValue:
                    amount = intValue.Amount;
                    return new INpcStat[]{intValue};
                
                case Behavior value:
                    for (int i = 0; i < npc.Behaviors.Length; i++)
                    {
                        if (npc.Behaviors[i].Data != value.Data) 
                            continue;
                        
                        var behavior = npc.Behaviors[i];
                        amount = behavior.Amount;
                        return new INpcStat[]{behavior};
                    }
                    break;
                
                case MentalState value:
                    for (int i = 0; i < npc.MentalStates.Length; i++)
                    {
                        if (npc.MentalStates[i].Data != value.Data) 
                            continue;
                        
                        var mentalState = npc.MentalStates[i];
                        amount = mentalState.Amount;
                        return new INpcStat[]{mentalState};
                    }
                    break;
                
                case NpcRelationship value:
                    GetSelectedNpcs(value.Npc, npc, out Npc[] npcs);
                    using (ListPool<INpcStat>.Get(out var list))
                    {
                        for (int i = 0; i < npc.Relationships.Length; i++)
                        {
                            for (int j = 0; j < npcs.Length; j++)
                            {
                                if (npc.Relationships[i].Npc != npcs[j])
                                    continue;
                                
                                list.Add(npc.Relationships[i]);
                            }
                        }

                        amount = -1;
                        return list.ToArray();
                    }
                    
            }
            amount = -1;
            return null;
        }
    }
}