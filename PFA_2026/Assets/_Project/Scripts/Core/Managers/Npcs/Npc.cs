using System.Linq;
using Naussilus.Core.Consequences;
using Naussilus.Core.NpcDatas;

namespace Naussilus.Core.Managers.Npcs
{
    public class Npc
    {
        public string Name { get; private set; }
        public NpcBehavior[] Behaviors { get; private set; }
        public NpcMentalState[] MentalStates { get; private set; }
        public EGender Gender { get; private set; }
        public NpcRelationship[] Relationships { get; private set; }
        public string CurrentThinking { get; private set; }
        
        public Npc(NpcData npcData)
        {
            Name = npcData.Name;
            Behaviors = npcData.Behavior.Select(b => b.Clone()).ToArray();
            MentalStates = npcData.MentalState.Select(m => m.Clone()).ToArray();
            Gender = npcData.Gender;
            Relationships = npcData.Relationships.Select(r => r.Clone()).ToArray();
            CurrentThinking = npcData.CurrentThinking;
        }

        public int GetRelationshipWith(NpcData npcData)
        {
            for (int i = 0; i < Relationships.Length; i++)
            {
                if (Relationships[i].Npc is NpcValue value)
                {
                    if (value.NpcData == npcData)
                    {
                        return Relationships[i].Amount;
                    }
                }
            }
            return -1;
        }
        
        public int[] GetValues(IConditionalEffect conditionalEffect)
        {
            switch (conditionalEffect)
            {
                case IntValue intValue:
                    return new []{intValue.Amount};
                
                case NpcBehavior value:
                    foreach (var behavior in Behaviors)
                        if (behavior.Behavior == value.Behavior)
                            return new []{behavior.Amount};
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in MentalStates)
                        if (mentalState.MentalState == value.MentalState)
                            return new []{mentalState.Amount};
                    break;
                
                case NpcRelationship value:
                    //NpcManager.GetSelectedNpcs(value.Npc, this);
                    break;
            }
            return null;
        }
        
        public void SetValue(IConsequenceValue consequenceValue, int amount)
        {
            switch (consequenceValue)
            {
                case NpcBehavior value:
                    foreach (var behavior in Behaviors)
                        if (behavior.Behavior == value.Behavior)
                            behavior.SetNewAmount(amount);
                    break;
                
                case NpcMentalState value:
                    foreach (var mentalState in MentalStates)
                        if (mentalState.MentalState == value.MentalState)
                            mentalState.SetNewAmount(amount);
                    break;
                
                case NpcRelationship value:
                    foreach (var relationship in Relationships)
                        if (relationship.Npc == value.Npc)
                            relationship.SetNewAmount(amount);
                    break;
            }
        }
    }
}