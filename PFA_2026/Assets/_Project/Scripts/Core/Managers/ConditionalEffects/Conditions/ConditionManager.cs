using System.Collections.Generic;
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
    public static class ConditionManager
    {
        public static void ComputeAllCondition(this Condition[] currentConditions, NpcData currentNpcData, out List<NpcData> validNpcs)
        {
            using (ListPool<NpcData>.Get(out var list))
            {
                for (int i = 0; i < currentConditions.Length; i++)
                {
                    var condition = currentConditions[i];
                    
                    NpcData[] leftNpcs = condition.IsLeftCurrentNpc 
                        ? new[] { currentNpcData } 
                        : NpcManager.GetSelectedNpcs(condition.LeftSubject, currentNpcData);

                    NpcData[] rightNpcs = condition.IsRightCurrentNpc 
                        ? new[] { currentNpcData } 
                        : NpcManager.GetSelectedNpcs(condition.RightSubject, currentNpcData);

                    for (var j = 0; j < leftNpcs.Length; j++)
                    {
                        var left = leftNpcs[j];
                        for (var k = 0; k < rightNpcs.Length; k++)
                        {
                            var right = rightNpcs[k]; 
                            if (!condition.ComputeCondition(left, right))
                                break;
                            list.Add(left);
                        }
                    }
                }
                validNpcs = list;
            }
        }
        
        public static void ComputeAllCondition(this Condition[] currentConditions, NpcData currentNpcData ,Category[] currentCategories, out List<NpcData> validNpcs)
                {
                    using (ListPool<NpcData>.Get(out var list))
                    {
                        for (int i = 0; i < currentConditions.Length; i++)
                        {
                            var condition = currentConditions[i];
                    
                            NpcData[] leftNpcs = condition.IsLeftCurrentNpc 
                                ? new[] { currentNpcData } 
                                : NpcManager.GetSelectedNpcs(condition.LeftSubject, currentNpcData, currentCategories);

                            NpcData[] rightNpcs = condition.IsRightCurrentNpc 
                                ? new[] { currentNpcData } 
                                : NpcManager.GetSelectedNpcs(condition.RightSubject, currentNpcData, currentCategories);

                            for (var j = 0; j < leftNpcs.Length; j++)
                            {
                                var left = leftNpcs[j];
                                for (var k = 0; k < rightNpcs.Length; k++)
                                {
                                    var right = rightNpcs[k]; 
                                    if (!condition.ComputeCondition(left, right))
                                        break;
                                    list.Add(left);
                                }
                            }
                        }
                        validNpcs = list;
                    }
                }

        private static bool ComputeCondition(this Condition condition, NpcData leftNpcData, NpcData rightNpcData)
        {
            NpcManager.TryGetNpc(leftNpcData.GUID, out Npc currentLeftNpc);
            NpcManager.TryGetNpc(rightNpcData.GUID, out Npc currentRightNpc);
            
            int[] leftSide = currentLeftNpc.GetValue(condition.Left);
            int[] rightSide = currentRightNpc.GetValue(condition.Right);
            
            if (leftSide.Contains(-1) || rightSide.Contains(-1))
            {
                Debug.LogError($"Negative value for condition {condition}");
                return false;
            }

            using (ListPool<bool>.Get(out var isAllValid))
            {
                for (int i = 0; i < leftSide.Length; i++)
                {
                    for (int j = 0; j < rightSide.Length; j++)
                    {
                        condition.IsValid(leftSide[i], rightSide[j], out bool isValid);
                        Debug.Log($"Condition {condition}: left: {leftSide}, right: {rightSide} return : {isValid}");
                    }
                }
                return isAllValid.All(valid => valid);
            }
        }

        private static bool IsValid(this Condition condition, int leftSide, int rightSide)
        {
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
            return isValid;
        }
        
        private static void IsValid(this Condition condition, int leftSide, int rightSide, out bool isValid)
        {
            bool isValidate = condition.ComparisonOperator switch
            {
                ComparisonOperator.Equal => leftSide == rightSide,
                ComparisonOperator.GreaterThan => leftSide > rightSide,
                ComparisonOperator.LessThan => leftSide < rightSide,
                ComparisonOperator.NotEqual => leftSide != rightSide,
                ComparisonOperator.GreaterThanOrEqual => leftSide >= rightSide,
                ComparisonOperator.LessThanOrEqual => leftSide <= rightSide,
                _ => false
            };
            isValid = isValidate;
        }
    }
}