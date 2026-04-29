using System.Collections.Generic;
using Naussilus.Core.Conditions;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.NpcDatas;
using UnityEngine;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers.Npcs
{
    public static class NpcManager
    {
        private static readonly Dictionary<string, NpcData> NpcDatas;
        private static readonly Dictionary<string, Npc> Npcs;
        private static readonly NpcData[] Entries;
        
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

            Entries = entries;
        }
        
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
        
        public static NpcData[] GetAllNpcs()
        {
            return Entries;
        }
        
        public static void GetAllNpcs(out NpcData[] allNpcs)
        {
            allNpcs = Entries;
        }

        public static NpcData[] GetSelectedNpcs(INpcSelector npcSelector , NpcData currentNpc ,Category[] currentCategories)
        {
            using (ListPool<NpcData>.Get(out var list))
            {
                NpcData[] result = new NpcData[] { };
                switch (npcSelector)
                {
                    case NpcValue npcValue:
                        return new[] { npcValue.NpcData };
                        
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
                        for (int j = 0; j < targetCategory.CurrentNpcs.Length; j++)
                        {
                            NpcData categoryNpc = targetCategory.CurrentNpcs[j];
                            list.Add(categoryNpc);
                        }
                        result = list.ToArray();
                        return result;
                }
                return result;
            }
        }
        public static NpcData[] GetSelectedNpcs(INpcSelector npcSelector , NpcData currentNpc)
        {
            using (ListPool<NpcData>.Get(out var list))
            {
                NpcData[] result = new NpcData[] { };
                switch (npcSelector)
                {
                    case NpcValue npcValue:
                        return new[] { npcValue.NpcData };

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
                    case NpcValue npcValue:
                        TryGetNpc(npcValue.NpcData.GUID, out Npc npc);
                        npcs = new[] { npc };
                        break;

                    case AllNpc:
                        var allNpcs = GetAllNpcs();
                        for (int j = 0; j < allNpcs.Length; j++)
                        {
                            TryGetNpc(allNpcs[j].GUID, out Npc allNpc);
                            if (allNpc == currentNpc)
                                continue;

                            list.Add(allNpc);
                        }

                        result = list.ToArray();
                        npcs = result;
                        break;

                    case Gender gender:
                        var npcDatas = GetAllNpcs();
                        for (int j = 0; j < npcDatas.Length; j++)
                        {
                            TryGetNpc(npcDatas[j].GUID, out Npc genderNpc);
                            if (genderNpc.Gender == gender.EGender)
                            {
                                list.Add(genderNpc);
                            }
                        }

                        result = list.ToArray();
                        npcs = result;
                        break;
                }
                npcs = result;
            }
        }
        
        public static INpcStat GetValue<T>(this Npc npc, T side, out int amount) where T : IConditionalEffectValue
        {
            switch (side)
            {
                case IntEffectValue intValue:
                    amount = intValue.Amount;
                    return intValue;
                
                case BehaviorValue value:
                    bool BehaviorPredicate(Behavior x) => x.Data == value.Stat;
                    var behavior = npc.Behaviors.Find(BehaviorPredicate);
                    amount = behavior.Amount;
                    return behavior;
                
                case MentalStateValue value:
                    bool MentalStatePredicate(MentalState x) => x.Data == value.Stat;
                    var mentalState = npc.MentalStates.Find(MentalStatePredicate);
                    amount = mentalState.Amount;
                    return mentalState;
                
                case NpcRelationshipData value:
                    bool RelationshipPredicate(NpcRelationship x) => x.Data == value;
                    var relationship = npc.Relationships.Find(RelationshipPredicate);
                    amount = relationship.Amount;
                    return relationship;
            }
            amount = -1;
            return null;
        }
    }
}