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
                    : NpcManager.GetSubjectNpcs(consequence.Subject, currentNpcData);

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
                    : NpcManager.GetSubjectNpcs(consequence.Subject, currentNpcData, currentCategories);

                foreach (var subject in subjects)
                    consequence.ComputeConsequence(subject);
            }
        }

        private static void ComputeConsequence(this Consequence consequence, NpcData currentNpcData)
        {
            ComputeValue(consequence, currentNpcData);
        }
        
        private static void ComputeValue(Consequence consequence, NpcData currentNpcData)
        {
            NpcManager.TryGetNpc(currentNpcData.GUID, out Npc currentNpc);
            IConsequenceValue stat = consequence.IntTarget;
            int leftSide = currentNpc.GetValue(stat);
            int rightSide = stat.Amount;

            int newAmount = consequence.ArithmeticOperator switch
            {
                ArithmeticOperator.Add => leftSide + rightSide,
                ArithmeticOperator.Subtract => leftSide - rightSide,
                ArithmeticOperator.Multiply => leftSide * rightSide,
                _ => leftSide
            };

            currentNpc.SetValue(stat, newAmount);
            Debug.Log(currentNpc.GetValue(stat).ToString());
        }
    }
}