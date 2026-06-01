using System;
using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Managements;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class ConditionalEffectManager
    {
        public static readonly List<Consequence> ValidConsequences = new List<Consequence>();
        public static Consequence lostConsequence;

        public static void AssignLostConsequence(Consequence consequence) => lostConsequence = consequence;
        
        private static Npc[] currentNpcs;

        private static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, Npc currentNpcData, Category[] currentCategories)
        {
            currentNpcs = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData, currentCategories)
                : new[] { currentNpcData };
            
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;
            
            for (int i = 0; i < currentNpcs.Length; i++)
            {
                currentConditions.ComputeAllCondition(currentNpcs[i], currentCategories, out var validNpcs);
                Debug.Log($"[ConditionalEffectManager] Conditions has been computed.");
                for (int j = 0; j < validNpcs.Count; j++)
                {
                    currentConsequences.ComputeAllConsequence(validNpcs[j], currentCategories);
                    Debug.Log($"[ConditionalEffectManager] Consequences has been computed.");
                }
            }
        }
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, Npc currentNpcData, out bool isGameLost)
        {
            currentNpcs = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;
            
            ValidConsequences.Clear();
            for (int i = 0; i < currentNpcs.Length; i++)
            {
                currentConditions.ComputeAllCondition(currentNpcs[i], out var validNpcs);
                for (int j = 0; j < validNpcs.Count; j++)
                {
                    isGameLost = currentConsequences.ComputeAllConsequence(validNpcs[j]);
                    if (isGameLost)
                        return;
                }
            }
            isGameLost = false;
        }
        public static bool ComputeOnlyConditions(this ConditionalEffect conditionalEffect, Npc currentNpcData)
        {
            currentNpcs = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            Condition[] currentConditions = conditionalEffect.Conditions;
            
            for (int i = 0; i < currentNpcs.Length; i++)
            {
                var isValid = currentConditions.ComputeAllCondition(currentNpcs[i]);
                if (!isValid)
                    return false;
            }
            return true;
        }

        public static void ComputeValidEffect(this RoomAction roomAction)
        {
            ValidConsequences.Clear();
            var actionEffects = roomAction.ActionEffects;
            var categories = roomAction.Categories;
            if (actionEffects == null || categories == null)
                return;
            
            for (int i = 0; i < actionEffects.Length; i++)
            {
                var actionEffect = actionEffects[i];
                var category = categories[actionEffect.CategoryIndex];
                
                for (int j = 0; j < category.CategoryNpcSlots.Length; j++)
                {
                    var currentNpc = category.CategoryNpcSlots[j].CurrentNpc;
                    var contains = currentNpc == actionEffect.Npc;
                    if (contains)
                    {
                        for (int k = 0; k < actionEffect.Effects?.Length; k++)
                        {
                            var effect = actionEffect.Effects[k];
                            effect.ComputeConditionalEffect(currentNpc, categories);
                        }
                    }
                }
            }
        }
    }
}