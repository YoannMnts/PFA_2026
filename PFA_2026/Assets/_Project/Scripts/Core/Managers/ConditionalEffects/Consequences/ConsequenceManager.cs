using System.Collections.Generic;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class ConsequenceManager
    {
        
        public static bool ComputeAllConsequence(this Consequence[] currentConsequence, Npc currentNpcData)
        {
            for (var i = 0; i < currentConsequence.Length; i++)
            {
                var consequence = currentConsequence[i];
                Npc[] subjects = consequence.ConsequenceSide.IsCurrentNpc
                    ? new[] { currentNpcData }
                    : NpcManager.GetSelectedNpcs(consequence.ConsequenceSide.Subject, currentNpcData);

                for (var j = 0; j < subjects.Length; j++)
                {
                    var subject = subjects[j];
                    var isGameLost = consequence.ComputeConsequence(subject);
                    if (isGameLost)
                        return false;
                }
            }
            return true;
        }
        
        public static bool ComputeAllConsequence(this Consequence[] currentConsequence, Npc currentNpcData ,Category[] currentCategories)
        {
            for (var i = 0; i < currentConsequence.Length; i++)
            {
                var consequence = currentConsequence[i];
                Npc[] subjects = consequence.ConsequenceSide.IsCurrentNpc
                    ? new[] { currentNpcData }
                    : NpcManager.GetSelectedNpcs(consequence.ConsequenceSide.Subject, currentNpcData, currentCategories);

                for (var j = 0; j < subjects.Length; j++)
                {
                    var subject = subjects[j];
                    var isGameLost = consequence.ComputeConsequence(subject);
                    if (isGameLost)
                        return false;
                }
            }
            return true;
        }

        private static bool ComputeConsequence(this Consequence consequence, Npc currentNpcData)
        {
            if (consequence.IsGameLost)
            {
                ConditionalEffectManager.AssignLostConsequence(consequence);
            }
            IConsequenceEffectValue stat = consequence.ConsequenceSide.Stat;
            var stats = currentNpcData.GetValue(stat);
            int rightSide = consequence.Amount;

            if (stats is null || rightSide < 0)
            {
                Debug.LogError($"[ConsequenceManager] Negative value for consequence left side : left: type is null, right: {rightSide}");
                return false;
            }
            for (int i = 0; i < stats.Length; i++)
            {
                consequence.ModifyValue(stats[i].Amount, rightSide, out var newAmount);
                Debug.Log($"[ConsequenceManager] Compute {stats[i]} : left: {stats[i].Amount} {consequence.ArithmeticOperator} right: {rightSide} return : {newAmount} for npc {currentNpcData.Name}");
                stats[i].SetNewAmount(newAmount);
            }

            for (int i = 0; i < consequence.Text.Length; i++)
            {
                var replace = consequence.Text[i].Replace("[CurrentNpc]", currentNpcData.Name);
                replace = replace.Replace("{CurrentNpc}", currentNpcData.Name);
                consequence.Text[i] = replace;
            }
            
            ConditionalEffectManager.ValidConsequences.Add(consequence);
            return true;
        }

        private static void ModifyValue(this Consequence consequence, int leftSide, int rightSide, out int newValue)
        {
            int newAmount = consequence.ArithmeticOperator switch
            {
                ArithmeticOperator.Add => leftSide + rightSide,
                ArithmeticOperator.Subtract => leftSide - rightSide,
                ArithmeticOperator.Multiply => leftSide * rightSide,
                _ => leftSide
            };
            newValue = Mathf.Clamp(newAmount, 0, 20);
        }
    }
}