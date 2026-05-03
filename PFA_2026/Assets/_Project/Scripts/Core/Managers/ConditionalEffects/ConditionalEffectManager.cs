using System;
using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Managers.Npcs;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class ConditionalEffectManager
    {
        public static event Action OnAddEffect;
        public static event Action OnRemoveEffect;
        
        private static List<ActionEffect> scheduledEffects = new List<ActionEffect>();
        
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
                for (int j = 0; j < validNpcs.Count; j++)
                    currentConsequences.ComputeAllConsequence(validNpcs[i], currentCategories);
            }
        }
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, Npc currentNpcData)
        {
            currentNpcs = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;
            
            for (int i = 0; i < currentNpcs.Length; i++)
            {
                currentConditions.ComputeAllCondition(currentNpcs[i], out var validNpcs);
                for (int j = 0; j < validNpcs.Count; j++)
                    currentConsequences.ComputeAllConsequence(validNpcs[i]);
            }
        }
        public static bool ComputeOnlyConditions(this ConditionalEffect conditionalEffect, Npc currentNpcData)
        {
            currentNpcs = conditionalEffect.IsEnumeration
                ? NpcManager.GetSelectedNpcs(conditionalEffect.CurrentNpcTarget, currentNpcData)
                : new[] { currentNpcData };
            Debug.Log($"[ConditionalEffectManager] Npcs selected : {currentNpcs.Length}");
            Condition[] currentConditions = conditionalEffect.Conditions;
            Consequence[] currentConsequences = conditionalEffect.Consequences;

            for (int i = 0; i < currentNpcs.Length; i++)
            {
                var isValid = currentConditions.ComputeAllCondition(currentNpcs[i]);
                if (!isValid)
                    return false;
            }
            return true;
        }

        public static void AddValidEffect(this RoomAction roomAction)
        {
            var actionEffects = roomAction.ActionEffects;
            var categories = roomAction.Categories;
            if (actionEffects == null || categories == null)
                return;
            
            for (int i = 0; i < actionEffects.Length; i++)
            {
                var actionEffect = actionEffects[i];
                var contains = categories[actionEffect.CategoryIndex].CurrentNpcs.Contains(actionEffect.Npc);
                if (contains)
                {
                    actionEffect.AddScheduledEffect();
                }
            }
        }
        
        public static void AddScheduledEffect(this ActionEffect actionEffect)
        {
            scheduledEffects.Add(actionEffect);
            actionEffect.Npc?.SetNewPosition(actionEffect.Position);
            OnAddEffect?.Invoke();
        }

        public static void RemoveScheduledEffect(this ActionEffect actionEffect)
        {
            scheduledEffects.Remove(actionEffect);
            actionEffect.Npc?.ReturnToLastPosition();
            OnRemoveEffect?.Invoke();
        }

        public static void ComputeScheduledEffects()
        {
            for (int i = 0; i < scheduledEffects.Count; i++)
            {
                var effects = scheduledEffects[i].Effects;
                var npc = scheduledEffects[i].Npc;
                var currentCategories = scheduledEffects[i].CurrentCategories;
                if (effects == null)
                    continue;
                for (int j = 0; j < effects.Length; j++)
                    effects[j].ComputeConditionalEffect(npc, currentCategories);
            }
        }
    }
}