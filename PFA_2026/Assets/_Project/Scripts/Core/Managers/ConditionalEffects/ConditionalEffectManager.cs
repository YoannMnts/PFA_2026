using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class ConditionalEffectManager
    {
        public static NpcData[] currentNpcsData { get; private set; }
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpcData, Category[] currentCategories)
        {
            currentNpcsData = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;
            
            for (int i = 0; i < currentNpcsData.Length; i++)
            {
                currentConditions.ComputeAllCondition(currentNpcData, currentCategories, out var validNpcs);
                for (int j = 0; j < validNpcs.Count; j++)
                    currentConsequences.ComputeAllConsequence(validNpcs[i], currentCategories);
            }
        }
        
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpcData)
        {
            currentNpcsData = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;

            for (int i = 0; i < currentNpcsData.Length; i++)
            {
                currentConditions.ComputeAllCondition(currentNpcData, out var validNpcs);
                for (int j = 0; j < validNpcs.Count; j++)
                    currentConsequences.ComputeAllConsequence(validNpcs[i]);
            }
        }
    }
}