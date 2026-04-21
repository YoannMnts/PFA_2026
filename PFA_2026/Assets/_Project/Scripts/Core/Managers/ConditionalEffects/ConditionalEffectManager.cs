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

            if (currentConditions.ComputeAllCondition(currentNpcData, currentCategories))
                currentConsequences.ComputeAllConsequence(currentNpcData, currentCategories);
        }
        
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpcData)
        {
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;

            if (currentConditions.ComputeAllCondition(currentNpcData))
                currentConsequences.ComputeAllConsequence(currentNpcData);
        }
    }
}