using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.NpcDatas;

namespace Naussilus.Core.Managers
{
    public static class ConditionalEffectManager
    {
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpcData, Category[] currentCategories)
        {
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;

            currentConditions.ComputeAllCondition(currentNpcData, currentCategories, out var validNpcs);
            
            for (int i = 0; i < validNpcs.Count; i++)
                currentConsequences.ComputeAllConsequence(validNpcs[i], currentCategories);
        }
        
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpcData)
        {
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;

            currentConditions.ComputeAllCondition(currentNpcData, out var validNpcs);
            
            for (int i = 0; i < validNpcs.Count; i++)
                currentConsequences.ComputeAllConsequence(validNpcs[i]);
        }
    }
}