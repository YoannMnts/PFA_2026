using Naussilus.Core.Managers.Npcs;
using UnityEngine;

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
                currentConditions.ComputeAllCondition(CurrentNpcs[i], currentCategories, out var validNpcs);
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
                currentConditions.ComputeAllCondition(CurrentNpcs[i], out var validNpcs);
                for (int j = 0; j < validNpcs.Count; j++)
                    currentConsequences.ComputeAllConsequence(validNpcs[i]);
            }
        }
        
        
        public static bool ComputeOnlyConditions(this ConditionalEffect conditionalEffect, Npc currentNpcData)
        {
            CurrentNpcs = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            Debug.Log($"[ConditionalEffectManager] Npcs selected : {CurrentNpcs.Length}");
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;

            for (int i = 0; i < CurrentNpcs.Length; i++)
            {
                var isValid = currentConditions.ComputeAllCondition(CurrentNpcs[i]);
                if (!isValid)
                    return false;
            }
            return true;
        }
    }
}