using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers
{
    public static class ConditionalEffectManager
    {
        private static Npc currentNpc;
        private static Condition[] currentConditions;
        private static Consequence[] currentConsequences;
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpcData, Category[] currentCategories)
        {
            currentConditions = conditionalEffect.Conditions;
            currentConsequences = conditionalEffect.Consequences;
            currentNpc = NpcManager.TryGetNpc(currentNpcData.GUID);

            ComputeAllCondtion(currentCategories);
        }

        public static void ComputeAllCondtion(Category[] currentCategories)
        {
            using (ListPool<bool>.Get(out var isAllConditionValidate))
            {
                for (int i = 0; i < currentConditions.Length; i++)
                {
                    if (currentConditions[i].IsCurrentNpc && currentConditions[i].ComputeCondition(currentNpc))
                        currentConsequences.ComputeAllConsequence(currentNpc, currentCategories);
                    
                    switch (currentConditions[i].Subject)
                    {
                        case NpcValue npcValue:
                            NpcManager.TryGetNpc(npcValue.NpcData.GUID, out Npc npc);
                            isAllConditionValidate.Add(currentConditions[i].ComputeCondition(npc)); 
                            continue;
                            
                        case AllNpc :
                            var allNpcs = NpcManager.GetAllNpcs();
                            for (int j = 0; j < allNpcs.Length; j++)
                            {
                                NpcManager.TryGetNpc(allNpcs[j].GUID, out Npc allNpc);
                                if (allNpc == currentNpc)
                                    continue;
                                isAllConditionValidate.Add(currentConditions[i].ComputeCondition(allNpc));
                            }
                            continue;
                        
                        case Gender gender:
                            var npcDatas = NpcManager.GetAllNpcs();
                            for (int j = 0; j < npcDatas.Length; j++)
                            {
                                if (npcDatas[j].Gender == gender.EGender)
                                {
                                    NpcManager.TryGetNpc(npcDatas[j].GUID, out Npc npcGender);
                                    isAllConditionValidate.Add(currentConditions[i].ComputeCondition(npcGender));
                                }
                            }
                            continue;
                        case CategoryIndex categoryIndex:
                            var targetCategory =  currentCategories[categoryIndex.Index];
                            for (int j = 0; j < targetCategory.CurrentNpcs.Length; j++)
                            {
                                Npc categoryNpc =  targetCategory.CurrentNpcs[j];
                                isAllConditionValidate.Add(currentConditions[i].ComputeCondition(categoryNpc));
                            }
                            continue;
                    }
                }
            }
        }

        public static int GetValue(this IConditionalEffect conditionValue, Npc npc)
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
    }
}