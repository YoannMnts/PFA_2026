using Naussilus.Core.Conditions;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static partial class ConditionalEffectManager
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
            
            switch (condition.ComparisonOperator)
            {
                case ComparisonOperator.Equal:
                    return leftSide == rightSide;
                case ComparisonOperator.GreaterThan:
                    return leftSide > rightSide;
                case ComparisonOperator.LessThan:
                    return leftSide < rightSide;
                case ComparisonOperator.NotEqual:
                    return leftSide != rightSide;
                case ComparisonOperator.GreaterThanOrEqual:
                    return leftSide >= rightSide;
                case ComparisonOperator.LessThanOrEqual:
                    return leftSide <= rightSide;
                default:
                    return false;
            }
        }
    }
}