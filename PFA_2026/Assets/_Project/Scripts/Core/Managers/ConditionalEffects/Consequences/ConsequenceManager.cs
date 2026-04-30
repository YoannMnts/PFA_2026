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
            for (var i = 0; i < currentConsequence.Length; i++)
            {
                var consequence = currentConsequence[i];
                NpcData[] subjects = consequence.ConsequenceSide.IsCurrentNpc
                    ? new[] { currentNpcData }
                    : NpcManager.GetSelectedNpcs(consequence.ConsequenceSide.Subject, currentNpcData);

                for (var j = 0; j < subjects.Length; j++)
                {
                    var subject = subjects[j];
                    consequence.ComputeConsequence(subject);
                }
            }
        }
        
        public static void ComputeAllConsequence(this Consequence[] currentConsequence, NpcData currentNpcData ,Category[] currentCategories)
        {
            for (var i = 0; i < currentConsequence.Length; i++)
            {
                var consequence = currentConsequence[i];
                NpcData[] subjects = consequence.ConsequenceSide.IsCurrentNpc
                    ? new[] { currentNpcData }
                    : NpcManager.GetSelectedNpcs(consequence.ConsequenceSide.Subject, currentNpcData, currentCategories);

                for (var j = 0; j < subjects.Length; j++)
                {
                    var subject = subjects[j];
                    consequence.ComputeConsequence(subject);
                }
            }
        }

        private static void ComputeConsequence(this Consequence consequence, NpcData currentNpcData)
        {
            NpcManager.TryGetNpc(currentNpcData.GUID, out Npc currentNpc);
            IConsequenceEffectValue stat = consequence.ConsequenceSide.Stat;
            var types = currentNpc.GetValue(stat);
            int rightSide = consequence.Amount;

            if (types is null || rightSide < 0)
            {
                Debug.LogError($"[ConsequenceManager] Negative value for consequence left side : left: type is null, right: {rightSide}");
                return;
            }
            for (int i = 0; i < types.Length; i++)
            {
                consequence.ModifyValue(types[i].Amount, rightSide, out var newAmount);
                types[i].SetNewAmount(newAmount);
                Debug.Log($"[ConsequenceManager] Compute : left: {types[i].Amount}, right: {rightSide} return : {newAmount} for npc {currentNpc.Name}");
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