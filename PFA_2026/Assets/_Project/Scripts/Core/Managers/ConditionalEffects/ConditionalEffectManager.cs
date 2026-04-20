using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;

namespace Naussilus.Core.Managers
{
    public static class ConditionalEffectManager
    {
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpcData, Category[] currentCategories)
        {
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;
            Npc currentNpc = NpcManager.TryGetNpc(currentNpcData.GUID);

            if (currentConditions.ComputeAllCondition(currentNpc, currentCategories))
                currentConsequences.ComputeAllConsequence(currentNpc, currentCategories);
        }
        
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpcData)
        {
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;
            Npc currentNpc = NpcManager.TryGetNpc(currentNpcData.GUID);

            if (currentConditions.ComputeAllCondition(currentNpc))
                currentConsequences.ComputeAllConsequence(currentNpc);
        }

        public static int GetValue(this IConditionalEffect conditionValue, Npc npc)
        {
            switch (conditionValue)
            {
                case IntValue intValue:
                    return intValue.Amount;
                
                case NpcBehavior value:
                    foreach (var behavior in npc.Behaviors)
                        if (behavior.Behavior == value.Behavior)
                            return behavior.Amount;
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in npc.MentalStates)
                        if (mentalState.MentalState == value.MentalState)
                            return mentalState.Amount;
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in npc.Relationships)
                        if (relationship.Npc == value.Npc)
                            return relationship.Amount;
                    break;
            }
            return -1;
        }
    }
}