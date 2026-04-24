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
            IConsequenceValue stat = consequence.IntTarget;
            int[] leftSide = currentNpc.GetValue(stat);
            int rightSide = consequence.Amount;

            if (leftSide.Contains(-1) || rightSide < 0)
            {
                Debug.LogError($"Negative value for consequence {consequence}");   
                return;
            }

            for (int i = 0; i < leftSide.Length; i++)
            {
                consequence.ModifyValue(leftSide[i], rightSide, out var newAmount);
                currentNpc.SetValue(stat, newAmount);
                Debug.Log($"Condition {consequence}: left: {leftSide}, right: {rightSide} return : {newAmount}");
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
            newValue = newAmount;
        }
    }
}