using System;
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
        public static bool ComputeAllCondition(this Condition[] currentConditions, NpcData currentNpcData, out List<NpcData> validNpcs)
        {
            
            validNpcs = new List<NpcData>();
            if(currentConditions.Length == 0)
            {
                validNpcs?.Add(currentNpcData);
                return true;
            }
            
            for (int i = 0; i < currentConditions.Length; i++)
            {
                var condition = currentConditions[i];
                
                NpcData[] leftNpcs = condition.LeftSide.IsCurrentNpc 
                    ? new[] { currentNpcData } 
                    : NpcManager.GetSelectedNpcs(condition.LeftSide.Subject, currentNpcData);

                NpcData[] rightNpcs = condition.RightSide.IsCurrentNpc 
                    ? new[] { currentNpcData } 
                    : NpcManager.GetSelectedNpcs(condition.RightSide.Subject, currentNpcData);

                for (var j = 0; j < leftNpcs.Length; j++)
                {
                    var currentLeftNpc = leftNpcs[j];
                    for (var k = 0; k < rightNpcs.Length; k++)
                    {
                        var currentRightNpc = rightNpcs[k];
                        if (!condition.ComputeCondition(currentLeftNpc, currentRightNpc, out var npcs))
                            continue;
                        validNpcs.AddRange(npcs);
                    }
                }
            }
            return validNpcs?.Count > 0;
        }
        public static bool ComputeAllCondition(this Condition[] currentConditions, NpcData currentNpcData)
        {
            for (int i = 0; i < currentConditions.Length; i++)
            {
                var condition = currentConditions[i];
                
                NpcData[] leftNpcs = condition.LeftSide.IsCurrentNpc 
                    ? new[] { currentNpcData } 
                    : NpcManager.GetSelectedNpcs(condition.LeftSide.Subject, currentNpcData);

                NpcData[] rightNpcs = condition.RightSide.IsCurrentNpc 
                    ? new[] { currentNpcData } 
                    : NpcManager.GetSelectedNpcs(condition.RightSide.Subject, currentNpcData);

                for (var j = 0; j < leftNpcs.Length; j++)
                {
                    var currentLeftNpc = leftNpcs[j];
                    for (var k = 0; k < rightNpcs.Length; k++)
                    {
                        var currentRightNpc = rightNpcs[k];
                        if (!condition.ComputeCondition(currentLeftNpc, currentRightNpc, out var npcs))
                            return false;
                    }
                }
            }
            return true;
        }
        public static bool ComputeAllCondition(this Condition[] currentConditions, NpcData currentNpcData ,Category[] currentCategories, out List<NpcData> validNpcs)
                {
                    validNpcs = new List<NpcData>();
                    if(currentConditions.Length == 0)
                    {
                        validNpcs?.Add(currentNpcData);
                        return true;
                    }
            
                    for (int i = 0; i < currentConditions.Length; i++)
                    {
                        var condition = currentConditions[i];
                
                        NpcData[] leftNpcs = condition.LeftSide.IsCurrentNpc 
                            ? new[] { currentNpcData } 
                            : NpcManager.GetSelectedNpcs(condition.LeftSide.Subject, currentNpcData, currentCategories);

                        NpcData[] rightNpcs = condition.RightSide.IsCurrentNpc 
                            ? new[] { currentNpcData } 
                            : NpcManager.GetSelectedNpcs(condition.RightSide.Subject, currentNpcData, currentCategories);

                        for (var j = 0; j < leftNpcs.Length; j++)
                        {
                            var currentLeftNpc = leftNpcs[j];
                            for (var k = 0; k < rightNpcs.Length; k++)
                            {
                                var currentRightNpc = rightNpcs[k];
                                if (!condition.ComputeCondition(currentLeftNpc, currentRightNpc, out var npcs))
                                    continue;
                                validNpcs.AddRange(npcs);
                            }
                        }
                    }
                    return validNpcs?.Count > 0;
                }
        
        private static bool ComputeCondition(this Condition condition, NpcData leftNpcData, NpcData rightNpcData, out List<NpcData> consequenceUsedNpc)
        {
            consequenceUsedNpc = new List<NpcData>();
            NpcManager.TryGetNpc(leftNpcData.GUID, out var leftNpc);
            NpcManager.TryGetNpc(rightNpcData.GUID, out var rightNpc);
            
            ConditionSide leftSide = condition.LeftSide;
            ConditionSide rightSide = condition.RightSide;
            
            var leftTypes = leftNpc.GetValue(leftSide.Stat);
            var rightTypes = rightNpc.GetValue(rightSide.Stat);

            for (int i = 0; i < leftTypes.Length; i++)
            {
                var leftType = leftTypes[i];
                for (int j = 0; j < rightTypes.Length; j++)
                {
                    var rightType = rightTypes[j];
                    condition.IsValid(leftType, rightType, out var valid);
                    if (!valid)
                        continue;
                    if (leftSide.UseRelationshipNpcToReturn)
                    {
                        var relationType = (NpcRelationship)leftType; 
                        consequenceUsedNpc = null; //consequenceUsedNpc.Add(relationType.Npc);
                        continue;
                    }
                    consequenceUsedNpc?.Add(leftNpcData);
                }
            }
            return consequenceUsedNpc?.Count > 0;
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
        
        private static void IsValid(this Condition condition, INpcStat leftType, INpcStat rightType, out bool isValid)
        {
            var leftSide = leftType.Amount;
            var rightSide = rightType.Amount;
            
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