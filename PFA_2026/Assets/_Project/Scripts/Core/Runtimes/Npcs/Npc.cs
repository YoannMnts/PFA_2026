using System.Linq;
using Naussilus.Core.NpcDatas;

namespace Naussilus.Core
{
    public class Npc : INpcSelector
    {
        public string Name { get; private set; }
        public Behavior[] Behaviors { get; private set; }
        public MentalState[] MentalStates { get; private set; }
        public EGender Gender { get; private set; }
        public NpcRelationship[] Relationships { get; private set; }
        public string CurrentThinking { get; private set; }
        
        public Npc(NpcData npcData)
        {
            Name = npcData.Name;
            Behaviors = npcData.Behavior?.Select(b => new Behavior(b)).ToArray();
            MentalStates = npcData.MentalState?.Select(m => new MentalState(m)).ToArray();
            Gender = npcData.Gender;
            CurrentThinking = npcData.CurrentThinking;
        }

        public void InitRelationships(NpcData npcData)
        {
            Relationships = npcData.Relationships?.Select(r => new NpcRelationship(r)).ToArray();
        }
        
        private int GetRelationshipWith(Npc npc)
        {
            for (int i = 0; i < Relationships.Length; i++)
            {
                if (Relationships[i].Npc == npc)
                {
                    return Relationships[i].Amount;
                }
            }
            return -1;
        }

        private void SetRelationshipWith(Npc npc, int amount)
        {
            for (int i = 0; i < Relationships.Length; i++)
            {
                if (Relationships[i].Npc == npc)
                {
                    Relationships[i].SetNewAmount(amount);;
                }
            }
        }
    }
}