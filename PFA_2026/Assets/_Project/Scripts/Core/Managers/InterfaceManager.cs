using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers;

namespace Naussilus.Core.Managers
{
    public static class InterfaceManager
    {
        public static INpcSelector GetNpcSelector(this INpcSelectorData data)
        {
            return data switch
            {
                AllNpcData => new AllNpc(),
                CategoryIndexData categoryIndexData => new CategoryIndex(categoryIndexData),
                GenderData genderData => new Gender(genderData),
                NpcValueData npcValue => NpcManager.TryGetNpc(npcValue.NpcData.GUID),
                _ => null
            };
        }

        public static IAnswer GetAnswer(this AnswerData data)
        {
            return data switch
            {
                BasicAnswerData basicAnswerData => new BasicAnswer(basicAnswerData),
                FinalAnswerData finalAnswerData => new FinalAnswer(finalAnswerData),
                _ => null
            };
        }

        public static IConsequenceEffectValue GetStat(this IConsequenceEffectValueData data)
        {
            return data switch
            {
                BehaviorValueData valueData => new Behavior(valueData),
                MentalStateValueData valueData => new MentalState(valueData),
                NpcRelationshipData relationshipData => new NpcRelationship(relationshipData),
                _ => null
            };
        }
        
        public static IConditionEffectValue GetStat(this IConditionEffectValueData data)
        {
            return data switch
            {
                IntValueData valueData => new IntValue(valueData),
                BehaviorValueData valueData => new Behavior(valueData),
                MentalStateValueData valueData => new MentalState(valueData),
                NpcRelationshipData relationshipData => new NpcRelationship(relationshipData),
                _ => null
            };
        }
    }
}