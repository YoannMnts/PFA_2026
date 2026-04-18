using Naussilus.Core.Consequences;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.Operators;

namespace Naussilus.Core.Managers
{
    public static partial class ConditionalEffectManager
    {
        private static void ComputeConsequence(this Consequence consequence, Npc currentNpc)
        {
            ComputeValue(consequence, currentNpc);
        }
        
        private static void ComputeValue(Consequence consequence, Npc currentNpc)
        {
            var stat = consequence.IntTarget;
            int leftSide = stat.GetValue(currentNpc);
            int rightSide = consequence.Amount;

            int amount;
            switch (consequence.ArithmeticOperator)
            {
                case ArithmeticOperator.Add:
                    amount = leftSide + rightSide;
                    currentNpc.SetValue(stat, amount);
                    break;
                case ArithmeticOperator.Subtract:
                    amount = leftSide - rightSide;
                    currentNpc.SetValue(stat, amount);
                    break;
                case ArithmeticOperator.Multiply:
                    amount = leftSide * rightSide;
                    currentNpc.SetValue(stat, amount);
                    break;
            }
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