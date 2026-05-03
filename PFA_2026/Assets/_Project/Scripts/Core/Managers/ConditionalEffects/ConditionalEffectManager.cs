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
        
        private static readonly List<ActionEffect> ScheduledEffects = new List<ActionEffect>();
        
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
                    currentConsequences.ComputeAllConsequence(validNpcs[i], currentCategories);
                    Debug.Log($"[ConditionalEffectManager] Consequences has been computed.");
                }
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
            Condition[] currentConditions = conditionalEffect.Conditions;
            
            for (int i = 0; i < currentNpcs.Length; i++)
            {
                var isValid = currentConditions.ComputeAllCondition(currentNpcs[i]);
                if (!isValid)
                    return false;
            }
            return true;
        }

        public static void AddAllValidEffect(this RoomAction roomAction)
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
            ScheduledEffects.Add(actionEffect);
            actionEffect.Npc?.SetNewPosition(actionEffect.Position);
            Debug.Log($"[ConditionalEffectManager] Adding scheduled effect {actionEffect.Npc?.Name}");
            OnAddEffect?.Invoke();
        }

        public static void RemoveScheduledEffect(this ActionEffect actionEffect)
        {
            ScheduledEffects.Remove(actionEffect);
            actionEffect.Npc?.ReturnToLastPosition();
            Debug.Log($"[ConditionalEffectManager] Removing scheduled effect {actionEffect.Npc?.Name}");
            OnRemoveEffect?.Invoke();
        }

        public static void ComputeScheduledEffects()
        {
            for (int i = 0; i < ScheduledEffects.Count; i++)
            {
                ConditionalEffect[] effects = ScheduledEffects[i].Effects;
                
                if (effects == null)
                    continue;
                
                Npc npc = ScheduledEffects[i].Npc;
                Category[] currentCategories = ScheduledEffects[i].CurrentCategories;
                
                for (int j = 0; j < effects.Length; j++)
                    effects[j].ComputeConditionalEffect(npc, currentCategories);
            }
        }
    }
}