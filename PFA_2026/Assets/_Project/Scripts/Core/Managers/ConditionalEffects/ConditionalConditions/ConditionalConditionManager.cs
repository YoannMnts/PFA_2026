using System.Linq;
using Naussilus.Core.Conditions;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.Operators;
using UnityEngine;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers
{
    public static class ConditionalConditionManager
    {
        public static bool ComputeAllCondition(this Condition[] currentConditions, Npc defaultNpc)
        {
            using (ListPool<bool>.Get(out var isAllConditionValidate))
            {
                for (int i = 0; i < currentConditions.Length; i++)
                {
                    if (currentConditions[i].IsCurrentNpc)
                        isAllConditionValidate.Add(currentConditions[i].ComputeCondition(defaultNpc));
                    
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
                                if (allNpc == defaultNpc)
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
                    }
                }
                return isAllConditionValidate.All(t => t);
            }
        }
        public static bool ComputeAllCondition(this Condition[] currentConditions, Npc defaultNpc ,Category[] currentCategories)
                {
                    using (ListPool<bool>.Get(out var isAllConditionValidate))
                    {
                        for (int i = 0; i < currentConditions.Length; i++)
                        {
                            if (currentConditions[i].IsCurrentNpc && currentConditions[i].ComputeCondition(defaultNpc))
                                isAllConditionValidate.Add(true);
                            
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
                                        if (allNpc == defaultNpc)
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
                        
                        return isAllConditionValidate.All(t => t);
                    }
                }

        private static bool ComputeCondition(this Condition condition, Npc currentNpc)
        {
            int leftSide = condition.Left.GetValue(currentNpc);
            int rightSide = condition.Right.GetValue(currentNpc);

            if (leftSide < 0 || rightSide < 0)
            {
                Debug.LogError($"Negative value for condition {condition}");   
                return false;
            }

            bool isValid = condition.ComparisonOperator switch
            {
                ComparisonOperator.Equal => leftSide == rightSide,
                ComparisonOperator.GreaterThan => leftSide > rightSide,
                ComparisonOperator.LessThan => leftSide < rightSide,
                ComparisonOperator.NotEqual => leftSide != rightSide,
                ComparisonOperator.GreaterThanOrEqual => leftSide >= rightSide,
                ComparisonOperator.LessThanOrEqual => leftSide <= rightSide,
                _ => false
            };
            Debug.Log($"Condition {condition}: left: {leftSide}, right: {rightSide} return : {isValid}");
            return isValid;
        }
    }
}