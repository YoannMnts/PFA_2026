using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class ConsequenceManager
    {
        public static void ComputeAllConsequence(this Consequence[] currentConsequence, Npc currentNpcData)
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
                    consequence.ComputeConsequence(subject);
                }
            }
        }
        
        public static void ComputeAllConsequence(this Consequence[] currentConsequence, Npc currentNpcData ,Category[] currentCategories)
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
                    consequence.ComputeConsequence(subject);
                }
            }
        }

        private static void ComputeConsequence(this Consequence consequence, Npc currentNpcData)
        {
            IConsequenceEffectValue stat = consequence.ConsequenceSide.Stat;
            var stast = currentNpcData.GetValue(stat);
            int rightSide = consequence.Amount;

            if (stast is null || rightSide < 0)
            {
                Debug.LogError($"[ConsequenceManager] Negative value for consequence left side : left: type is null, right: {rightSide}");
                return;
            }
            for (int i = 0; i < stast.Length; i++)
            {
                consequence.ModifyValue(stast[i].Amount, rightSide, out var newAmount);
                stast[i].SetNewAmount(newAmount);
                Debug.Log($"[ConsequenceManager] Compute : left: {stast[i].Amount}, right: {rightSide} return : {newAmount} for npc {currentNpcData.Name}");
            }
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