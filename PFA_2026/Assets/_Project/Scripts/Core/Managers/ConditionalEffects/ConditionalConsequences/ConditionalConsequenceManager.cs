using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.Operators;

namespace Naussilus.Core.Managers
{
    public static class ConditionalConsequenceManager
    {
        public static void ComputeAllConsequence(this Consequence[] currentConsequence, Npc currentNpc ,Category[] currentCategories)
        {
            for (int i = 0; i < currentConsequence.Length; i++)
            {
                if (currentConsequence[i].IsCurrentNpc)
                {
                    currentConsequence[i].ComputeConsequence(currentNpc);
                    continue;
                }

                switch (currentConsequence[i].Subject)
                {
                    case NpcValue npcValue:
                        NpcManager.TryGetNpc(npcValue.NpcData.GUID, out Npc npc);
                        currentConsequence[i].ComputeConsequence(npc);
                        continue;
                        
                    case AllNpc :
                        var allNpcs = NpcManager.GetAllNpcs();
                        for (int j = 0; j < allNpcs.Length; j++)
                        {
                            NpcManager.TryGetNpc(allNpcs[j].GUID, out Npc allNpc);
                            if (allNpc == currentNpc)
                                continue;
                            currentConsequence[i].ComputeConsequence(allNpc);
                        }
                        continue;
                    case Gender gender:
                        var npcDatas = NpcManager.GetAllNpcs();
                        for (int j = 0; j < npcDatas.Length; j++)
                        {
                            if (npcDatas[j].Gender == gender.EGender)
                            {
                                NpcManager.TryGetNpc(npcDatas[j].GUID, out Npc npcGender);
                                currentConsequence[i].ComputeConsequence(npcGender);
                            }
                        }
                        continue;
                    case CategoryIndex categoryIndex:
                        var targetCategory =  currentCategories[categoryIndex.Index];
                        for (int j = 0; j < targetCategory.CurrentNpcs.Length; j++)
                        {
                            var categoryNpc =  targetCategory.CurrentNpcs[j];
                            currentConsequence[i].ComputeConsequence(categoryNpc);
                        }
                        continue;
                }
            }
        }
        
        public static void ComputeConsequence(this Consequence consequence, Npc currentNpc)
        {
            ComputeValue(consequence, currentNpc);
        }
        
        private static void ComputeValue(Consequence consequence, Npc currentNpc)
        {
            IConsequenceValue stat = consequence.IntTarget;
            int leftSide = stat.GetValue(currentNpc);
            int rightSide = stat.Amount;

            int newAmount = consequence.ArithmeticOperator switch
            {
                ArithmeticOperator.Add => leftSide + rightSide,
                ArithmeticOperator.Subtract => leftSide - rightSide,
                ArithmeticOperator.Multiply => leftSide * rightSide,
                _ => leftSide
            };

            currentNpc.SetValue(stat, newAmount);
        }
        
        private static void SetValue(this Npc currentNpc, IConsequenceValue consequenceValue, int amount)
        {
            switch (consequenceValue)
            {
                case NpcBehavior value:
                    foreach (var behavior in currentNpc.Behaviors)
                        if (behavior.Behavior == value.Behavior)
                            behavior.SetNewAmount(amount);
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in currentNpc.MentalStates)
                        if (mentalState.MentalState == value.MentalState)
                            mentalState.SetNewAmount(amount);
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in currentNpc.Relationships)
                        if (relationship.Npc == value.Npc)
                            relationship.SetNewAmount(amount);
                    break;
            }
        }
    }
}