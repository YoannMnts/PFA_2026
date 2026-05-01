using Naussilus.Core.Managers.Npcs;

namespace Naussilus.Core.Managers
{
    public static class ConditionalEffectManager
    {
        private static Npc[] CurrentNpcs { get; set; }
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, Npc currentNpcData, Category[] currentCategories)
        {
            CurrentNpcs = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;
            
            for (int i = 0; i < CurrentNpcs.Length; i++)
            {
                currentConditions.ComputeAllCondition(currentNpcData, currentCategories, out var validNpcs);
                for (int j = 0; j < validNpcs.Count; j++)
                    currentConsequences.ComputeAllConsequence(validNpcs[i], currentCategories);
            }
        }
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, Npc currentNpcData)
        {
            CurrentNpcs = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;
            
            for (int i = 0; i < CurrentNpcs.Length; i++)
            {
                currentConditions.ComputeAllCondition(currentNpcData, out var validNpcs);
                for (int j = 0; j < validNpcs.Count; j++)
                    currentConsequences.ComputeAllConsequence(validNpcs[i]);
            }
        }
        
        
        public static bool ComputeOnlyConditions(this ConditionalEffect conditionalEffect, Npc currentNpcData)
        {
            CurrentNpcs = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;

            for (int i = 0; i < CurrentNpcs.Length; i++)
            {
                var isValid = currentConditions.ComputeAllCondition(currentNpcData);
                if (!isValid)
                    return false;
            }
            return true;
        }
    }
}