using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;

namespace Naussilus.Core.Managers
{
    public static class ConditionalEffectManager
    {
        private static NpcData[] CurrentNpcsData { get; set; }
        public static void ComputeConditionalEffect(this ConditionalEffectData conditionalEffect, NpcData currentNpcData, CategoryData[] currentCategories)
        {
            CurrentNpcsData = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            ConditionData[] currentConditions = conditionalEffect.Conditions;
            ConsequenceData[] currentConsequences = conditionalEffect.Consequences;
            
            for (int i = 0; i < CurrentNpcsData.Length; i++)
            {
                currentConditions.ComputeAllCondition(currentNpcData, currentCategories, out var validNpcs);
                for (int j = 0; j < validNpcs.Count; j++)
                    currentConsequences.ComputeAllConsequence(validNpcs[i], currentCategories);
            }
        }
        public static void ComputeConditionalEffect(this ConditionalEffectData conditionalEffect, NpcData currentNpcData)
        {
            CurrentNpcsData = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            ConditionData[] currentConditions = conditionalEffect.Conditions;
            ConsequenceData[] currentConsequences = conditionalEffect.Consequences;
            
            for (int i = 0; i < CurrentNpcsData.Length; i++)
            {
                currentConditions.ComputeAllCondition(currentNpcData, out var validNpcs);
                for (int j = 0; j < validNpcs.Count; j++)
                    currentConsequences.ComputeAllConsequence(validNpcs[i]);
            }
        }
        
        
        public static bool ComputeOnlyConditions(this ConditionalEffectData conditionalEffect, NpcData currentNpcData)
        {
            CurrentNpcsData = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            ConditionData[] currentConditions = conditionalEffect.Conditions;
            ConsequenceData[] currentConsequences = conditionalEffect.Consequences;

            for (int i = 0; i < CurrentNpcsData.Length; i++)
            {
                var isValid = currentConditions.ComputeAllCondition(currentNpcData);
                if (!isValid)
                    return false;
            }
            return true;
        }
    }
}