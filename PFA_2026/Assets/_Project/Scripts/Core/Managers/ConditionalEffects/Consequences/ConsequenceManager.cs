using System.Linq;
using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Managers
{
    public static class ConsequenceManager
    {
        public static void ComputeAllConsequence(this Consequence[] currentConsequence, NpcData currentNpcData)
        {
            foreach (var consequence in currentConsequence)
            {
                NpcData[] subjects = consequence.IsCurrentNpc
                    ? new[] { currentNpcData }
                    : NpcManager.GetSelectedNpcs(consequence.Subject, currentNpcData);

                foreach (var subject in subjects)
                    consequence.ComputeConsequence(subject);
            }
        }
        
        public static void ComputeAllConsequence(this Consequence[] currentConsequence, NpcData currentNpcData ,Category[] currentCategories)
        {
            foreach (var consequence in currentConsequence)
            {
                NpcData[] subjects = consequence.IsCurrentNpc
                    ? new[] { currentNpcData }
                    : NpcManager.GetSelectedNpcs(consequence.Subject, currentNpcData, currentCategories);

                foreach (var subject in subjects)
                    consequence.ComputeConsequence(subject);
            }
        }

        private static void ComputeConsequence(this Consequence consequence, NpcData currentNpcData)
        {
            NpcManager.TryGetNpc(currentNpcData.GUID, out Npc currentNpc);
            IConsequenceEffectValue stat = consequence.IntTarget;
            var type =currentNpc.GetValue(stat, out var amount);
            int rightSide = consequence.Amount;

            if (amount > 0 || rightSide < 0)
            {
                Debug.LogError($"[ConsequenceManager] Negative value for consequence left side : left: {amount}, right: {rightSide}");
                return;
            }

            consequence.ModifyValue(amount, rightSide, out var newAmount);
            type.SetNewAmount(newAmount);
            Debug.Log($"[ConsequenceManager] Compute : left: {amount}, right: {rightSide} return : {newAmount} for npc {currentNpc.Name}");
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