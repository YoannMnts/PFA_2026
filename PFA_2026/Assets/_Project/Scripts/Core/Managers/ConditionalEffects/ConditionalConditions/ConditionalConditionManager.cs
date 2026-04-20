using Naussilus.Core.Conditions;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class ConditionalConditionManager
    {
        public static bool ComputeCondition(this Condition condition, Npc currentNpc)
        {
            int leftSide = condition.Left.GetValue(currentNpc);
            int rightSide = condition.Right.GetValue(currentNpc);

            if (leftSide < 0 || rightSide < 0)
            {
                Debug.LogError($"Negative value for condition {condition}");   
                return false;
            }

            return condition.ComparisonOperator switch
            {
                ComparisonOperator.Equal => leftSide == rightSide,
                ComparisonOperator.GreaterThan => leftSide > rightSide,
                ComparisonOperator.LessThan => leftSide < rightSide,
                ComparisonOperator.NotEqual => leftSide != rightSide,
                ComparisonOperator.GreaterThanOrEqual => leftSide >= rightSide,
                ComparisonOperator.LessThanOrEqual => leftSide <= rightSide,
                _ => false
            };
        }
    }
}