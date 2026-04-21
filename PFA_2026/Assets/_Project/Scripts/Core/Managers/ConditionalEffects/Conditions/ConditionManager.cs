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
        public static bool ComputeAllCondition(this Condition[] currentConditions, NpcData currentNpcData)
        {
            using (ListPool<bool>.Get(out var isAllConditionValidate))
            {
                for (int i = 0; i < currentConditions.Length; i++)
                {
                    if (currentConditions[i].IsCurrentNpc)
                        isAllConditionValidate.Add(currentConditions[i].ComputeCondition(currentNpcData));
                    
                    var npcSubjects = NpcManager.GetSubjectNpcs(currentConditions[i].LeftSubject, currentNpcData);
                    for (int j = 0; j < npcSubjects.Length; j++)
                    {
                        isAllConditionValidate.Add(currentConditions[i].ComputeCondition(currentNpcData));
                    }
                }
                return isAllConditionValidate.All(t => t);
            }
        }
        public static bool ComputeAllCondition(this Condition[] currentConditions, NpcData currentNpcData ,Category[] currentCategories)
                {
                    using (ListPool<bool>.Get(out var isAllConditionValidate))
                    {
                        for (int i = 0; i < currentConditions.Length; i++)
                        {
                            if (currentConditions[i].IsCurrentNpc)
                                isAllConditionValidate.Add(currentConditions[i].ComputeCondition(currentNpcData));
                            
                            var leftNpcSubjects = NpcManager.GetSubjectNpcs(currentConditions[i].LeftSubject, currentNpcData, currentCategories);
                            for (int j = 0; j < leftNpcSubjects.Length; j++)
                            {
                                isAllConditionValidate.Add(currentConditions[i].ComputeCondition(currentNpcData));
                            }
                        }
                        
                        return isAllConditionValidate.All(t => t);
                    }
                }

        private static bool ComputeCondition(this Condition condition, NpcData leftNpcData/*, NpcData rightNpcData*/)
        {
            /*
            NpcManager.TryGetNpc(leftNpcData.GUID, out Npc leftNpc);
            NpcManager.TryGetNpc(rightNpcData.GUID, out Npc rightNpc);
            int leftSide = leftNpc.GetValue(condition.Left);
            int rightSide = rightNpc.GetValue(condition.Right);

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
            */
            return false;
        }
    }
}