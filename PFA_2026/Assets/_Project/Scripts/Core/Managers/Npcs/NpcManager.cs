using System.Collections.Generic;
using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.NpcDatas;
using Sirenix.Utilities;
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

                var npc = new Npc(entry);
                Npcs.Add(entry.GUID, npc);
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

        public static NpcData[] GetSubjectNpcs(INpcSelector npcSelector , NpcData currentNpc ,Category[] currentCategories)
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
                        result.AddRange(list);
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
                        result.AddRange(list);
                        return result;
                    
                    case CategoryIndex categoryIndex:
                        var targetCategory =  currentCategories[categoryIndex.Index];
                        for (int j = 0; j < targetCategory.CurrentNpcs.Length; j++)
                        {
                            NpcData categoryNpc = targetCategory.CurrentNpcs[j];
                            list.Add(categoryNpc);
                        }
                        result.AddRange(list);
                        return result;
                }
                return result;
            }
        }
        public static NpcData[] GetSubjectNpcs(INpcSelector npcSelector , NpcData currentNpc)
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

                        result.AddRange(list);
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

                        result.AddRange(list);
                        return result;
                }
                return result;
            }
        }
        
        public static int GetValue(this Npc npc ,IConditionalEffect conditionValue)
        {
            switch (conditionValue)
            {
                case IntValue intValue:
                    return intValue.Amount;
                
                case NpcBehavior value:
                    foreach (var behavior in npc.Behaviors)
                        if (behavior.Behavior == value.Behavior)
                            return behavior.Amount;
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in npc.MentalStates)
                        if (mentalState.MentalState == value.MentalState)
                            return mentalState.Amount;
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in npc.Relationships)
                        if (relationship.Npc == value.Npc)
                            return relationship.Amount;
                    break;
            }
            return -1;
        }
        
        public static void SetValue(this Npc npc, IConsequenceValue consequenceValue, int amount)
        {
            switch (consequenceValue)
            {
                case NpcBehavior value:
                    foreach (var behavior in npc.Behaviors)
                        if (behavior.Behavior == value.Behavior)
                            behavior.SetNewAmount(amount);
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in npc.MentalStates)
                        if (mentalState.MentalState == value.MentalState)
                            mentalState.SetNewAmount(amount);
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in npc.Relationships)
                        if (relationship.Npc == value.Npc)
                            relationship.SetNewAmount(amount);
                    break;
            }
        }
    }
}