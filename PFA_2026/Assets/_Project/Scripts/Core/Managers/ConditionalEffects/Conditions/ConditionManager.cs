using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Conditions;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.Operators;
using NUnit.Framework;
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
                        : NpcManager.GetSubjectNpcs(condition.LeftSubject, currentNpcData);

                    NpcData[] rightNpcs = condition.IsRightCurrentNpc 
                        ? new[] { currentNpcData } 
                        : NpcManager.GetSubjectNpcs(condition.RightSubject, currentNpcData);

                    for (var j = 0; j < leftNpcs.Length; j++)
                    {
                        var left = leftNpcs[j];
                        for (var k = 0; k < rightNpcs.Length; k++)
                        {
                            var right = rightNpcs[k];
                            condition.ComputeCondition(left, right, out var validNpc);
                            if (validNpc == null)
                                continue;
                            list.Add(validNpc);
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
                                : NpcManager.GetSubjectNpcs(condition.LeftSubject, currentNpcData, currentCategories);

                            NpcData[] rightNpcs = condition.IsRightCurrentNpc 
                                ? new[] { currentNpcData } 
                                : NpcManager.GetSubjectNpcs(condition.RightSubject, currentNpcData, currentCategories);

                            for (var j = 0; j < leftNpcs.Length; j++)
                            {
                                var left = leftNpcs[j];
                                for (var k = 0; k < rightNpcs.Length; k++)
                                {
                                    var right = rightNpcs[k];
                                    condition.ComputeCondition(left, right, out var validNpc);
                                    //NEED TO REWORK !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                    if (validNpc == null)
                                    {
                                        if (list.Contains(left))
                                        {
                                            list.Remove(left);
                                        }
                                        continue;
                                    }
                                    list.Add(validNpc);
                                }
                            }
                        }
                        validNpcs = list;
                    }
                }

        private static void ComputeCondition(this Condition condition, NpcData leftNpcData, NpcData rightNpcData, out NpcData npcResult)
        {
            NpcManager.TryGetNpc(leftNpcData.GUID, out Npc leftNpc);
            NpcManager.TryGetNpc(rightNpcData.GUID, out Npc rightNpc);
            int leftSide = leftNpc.GetValue(condition.Left);
            int rightSide = rightNpc.GetValue(condition.Right);

            if (leftSide < 0 || rightSide < 0)
            {
                Debug.LogError($"Negative value for condition {condition}");
                npcResult = null;
                return;
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
            if (!isValid)
            {
                npcResult = null;
                return;
            }
            npcResult = leftNpcData;
        }
    }
}