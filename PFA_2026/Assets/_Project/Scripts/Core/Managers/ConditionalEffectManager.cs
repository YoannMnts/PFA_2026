using Naussilus.Core.NpcDatas;
using Naussilus.Core.Operators;
using Naussilus.Gameplay.Npcs;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class ConditionalEffectManager
    {
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpc)
        {
            //Ajouter le INpcSelector pour choisir le/les npc
            NpcManager.TryGetNpc(currentNpc.GUID, out Npc npc);
            if (conditionalEffect.AreConditionsMet(npc))
            {
                Consequence[] consequences = conditionalEffect.Consequences;
                for (int i = 0; i < consequences.Length; i++)
                {
                    if (consequences[i].IsCurrentNpc)
                    {
                        consequences[i].ComputeConsequence(npc);
                    }
                }
            }
        }
        
        public static void ComputeConditionalEffect(Condition condition, Consequence consequence){}
        
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

        private static void ComputeConsequence(this Consequence consequence, Npc npc)
        {
            var stat = consequence.IntTarget;
            int leftSide = stat.GetValue(npc);
            int rightSide = consequence.Amount;

            int amount;
            switch (consequence.ArithmeticOperator)
            {
                case ArithmeticOperator.Add:
                    amount = leftSide + rightSide;
                    npc.SetValue(stat, amount);
                    break;
                case ArithmeticOperator.Subtract:
                    amount = leftSide - rightSide;
                    npc.SetValue(stat, amount);
                    break;
                case ArithmeticOperator.Multiply:
                    amount = leftSide * rightSide;
                    npc.SetValue(stat, amount);
                    break;
            }
        }
        
        private static int GetValue(this IConditionValue conditionValue, Npc currentNpc)
        {
            switch (conditionValue)
            {
                case NpcBehavior value:
                    foreach (var behavior in currentNpc.Behaviors)
                        if (behavior.Stat == value.Stat)
                            return behavior.Amount;
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in currentNpc.MentalStates)
                        if (mentalState.Gauge == value.Gauge)
                            return mentalState.Amount;
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in currentNpc.Relationships)
                        if (relationship.Npc == value.Npc)
                            return relationship.Amount;
                    break;
            }
            return -1;
        }
        
        private static int GetValue(this IConsequenceValue consequenceValue, Npc currentNpc)
        {
            switch (consequenceValue)
            {
                case NpcBehavior value:
                    foreach (var behavior in currentNpc.Behaviors)
                        if (behavior.Stat == value.Stat)
                            return behavior.Amount;
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in currentNpc.MentalStates)
                        if (mentalState.Gauge == value.Gauge)
                            return mentalState.Amount;
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in currentNpc.Relationships)
                        if (relationship.Npc == value.Npc)
                            return relationship.Amount;
                    break;
            }
            return -1;
        }
        
        private static void SetValue(this Npc currentNpc, IConsequenceValue consequenceValue, int amount)
        {
            switch (consequenceValue)
            {
                case NpcBehavior value:
                    foreach (var behavior in currentNpc.Behaviors)
                        if (behavior.Stat == value.Stat)
                            behavior.SetNewAmount(amount);
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in currentNpc.MentalStates)
                        if (mentalState.Gauge == value.Gauge)
                            mentalState.SetNewAmount(amount);
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in currentNpc.Relationships)
                        if (relationship.Npc == value.Npc)
                            relationship.SetNewAmount(amount);
                    break;
            }
        }
    }
}