using System.Collections.Generic;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class ConditionManager
    {
        public static bool ComputeAllCondition(this Condition[] currentConditions, Npc currentNpcData, out List<Npc> validNpcs)
        {
            
            validNpcs = new List<Npc>();
            if(currentConditions.Length == 0)
            {
                validNpcs?.Add(currentNpcData);
                return true;
            }
            
            for (int i = 0; i < currentConditions.Length; i++)
            {
                var condition = currentConditions[i];
                
                Npc[] leftNpcs = condition.LeftSide.IsCurrentNpc 
                    ? new[] { currentNpcData } 
                    : NpcManager.GetSelectedNpcs(condition.LeftSide.Subject, currentNpcData);

                Npc[] rightNpcs = condition.RightSide.IsCurrentNpc 
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
        public static bool ComputeAllCondition(this Condition[] currentConditions, Npc currentNpcData)
        {
            for (int i = 0; i < currentConditions.Length; i++)
            {
                var condition = currentConditions[i];
                
                Npc[] leftNpcs = condition.LeftSide.IsCurrentNpc 
                    ? new[] { currentNpcData } 
                    : NpcManager.GetSelectedNpcs(condition.LeftSide.Subject, currentNpcData);
                Debug.Log($"[ConditionManager] Left Npcs selected : {leftNpcs.Length}");

                Npc[] rightNpcs = condition.RightSide.IsCurrentNpc 
                    ? new[] { currentNpcData } 
                    : NpcManager.GetSelectedNpcs(condition.RightSide.Subject, currentNpcData);
                Debug.Log($"[ConditionManager] Right Npcs selected : {rightNpcs.Length}, Current Npc : {currentNpcData.Name}");

                
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
        public static bool ComputeAllCondition(this Condition[] currentConditions, Npc currentNpcData ,Category[] currentCategories, out List<Npc> validNpcs)
                {
                    validNpcs = new List<Npc>();
                    if(currentConditions.Length == 0)
                    {
                        validNpcs?.Add(currentNpcData);
                        return true;
                    }
            
                    for (int i = 0; i < currentConditions.Length; i++)
                    {
                        var condition = currentConditions[i];
                
                        Npc[] leftNpcs = condition.LeftSide.IsCurrentNpc 
                            ? new[] { currentNpcData } 
                            : NpcManager.GetSelectedNpcs(condition.LeftSide.Subject, currentNpcData, currentCategories);

                        Npc[] rightNpcs = condition.RightSide.IsCurrentNpc 
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
        
        private static bool ComputeCondition(this Condition condition, Npc leftNpcData, Npc rightNpcData, out List<Npc> consequenceUsedNpc)
        {
            consequenceUsedNpc = new List<Npc>();
            
            ConditionSide leftSide = condition.LeftSide;
            ConditionSide rightSide = condition.RightSide;
            
            var leftStats = leftNpcData.GetValue(leftSide.Stat);
            var rightStats = rightNpcData.GetValue(rightSide.Stat);

            for (int i = 0; i < leftStats.Length; i++)
            {
                var leftType = leftStats[i];
                for (int j = 0; j < rightStats.Length; j++)
                {
                    var rightType = rightStats[j];
                    condition.IsValid(leftType, rightType, out var valid);
                    if (!valid)
                        continue;
                    if (leftSide.UseRelationshipNpcToReturn)
                    {
                        var relationType = (NpcRelationship)leftType; 
                        consequenceUsedNpc?.Add(relationType.Npc);
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