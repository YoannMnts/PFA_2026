using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;

namespace Naussilus.Core.Managers
{
    public static partial class ConditionalEffectManager
    {
        public static void ComputeConditionalEffect(this ConditionalEffect conditionalEffect, NpcData currentNpcData, Category[] currentCategories)
        {
            NpcManager.TryGetNpc(currentNpcData.GUID, out Npc currentNpc);
            if (conditionalEffect.AreConditionsMet(currentNpc))
            {
                Consequence[] consequences = conditionalEffect.Consequences;
                for (int i = 0; i < consequences.Length; i++)
                {
                    if (consequences[i].IsCurrentNpc)
                    {
                        consequences[i].ComputeConsequence(currentNpc);
                        continue;
                    }

                    switch (consequences[i].Subject)
                    {
                        case NpcValue npcValue:
                            NpcManager.TryGetNpc(npcValue.NpcData.GUID, out Npc npc);
                            consequences[i].ComputeConsequence(npc);
                            continue;
                        case AllNpc :
                            var allNpcs = NpcManager.GetAllNpcs();
                            for (int j = 0; j < allNpcs.Length; j++)
                            {
                                NpcManager.TryGetNpc(allNpcs[j].GUID, out Npc allNpc);
                                consequences[i].ComputeConsequence(allNpc);
                            }
                            continue;
                        case Gender gender:
                            var npcDatas = NpcManager.GetAllNpcs();
                            for (int j = 0; j < npcDatas.Length; j++)
                            {
                                if (npcDatas[j].Gender == gender.EGender)
                                {
                                    NpcManager.TryGetNpc(npcDatas[j].GUID, out Npc npcGender);
                                    consequences[i].ComputeConsequence(npcGender);
                                }
                            }
                            continue;
                        case CategoryIndex categoryIndex:
                            var targetCategory =  currentCategories[categoryIndex.Index];
                            for (int j = 0; j < targetCategory.CurrentNpcs.Length; j++)
                            {
                                var categoryNpc =  targetCategory.CurrentNpcs[j];
                                consequences[i].ComputeConsequence(categoryNpc);
                            }
                            continue;
                    }
                }
            }
        }
        

        private static int GetValue(this IConditionValue conditionValue, Npc currentNpc)
        {
            switch (conditionValue)
            {
                case NpcBehavior value:
                    foreach (var behavior in currentNpc.Behaviors)
                        if (behavior.Stat == value.Stat)
                            return behavior.Amount;
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in currentNpc.MentalStates)
                        if (mentalState.Gauge == value.Gauge)
                            return mentalState.Amount;
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in currentNpc.Relationships)
                        if (relationship.Npc == value.Npc)
                            return relationship.Amount;
                    break;
            }
            return -1;
        }
        
        private static int GetValue(this IConsequenceValue consequenceValue, Npc currentNpc)
        {
            switch (consequenceValue)
            {
                case NpcBehavior value:
                    foreach (var behavior in currentNpc.Behaviors)
                        if (behavior.Stat == value.Stat)
                            return behavior.Amount;
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in currentNpc.MentalStates)
                        if (mentalState.Gauge == value.Gauge)
                            return mentalState.Amount;
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in currentNpc.Relationships)
                        if (relationship.Npc == value.Npc)
                            return relationship.Amount;
                    break;
            }
            return -1;
        }
    }
}