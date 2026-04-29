using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Consequences;
using Naussilus.Core.NpcDatas;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers.Npcs
{
    public class Npc
    {
        public string Name { get; private set; }
        public List<Behavior> Behaviors { get; private set; }
        public List<MentalState> MentalStates { get; private set; }
        public EGender Gender { get; private set; }
        public List<NpcRelationship> Relationships { get; private set; }
        public string CurrentThinking { get; private set; }
        
        public Npc(NpcData npcData)
        {
            Name = npcData.Name;
            Behaviors = npcData.Behavior.Select(b => new Behavior(b)).ToList();
            MentalStates = npcData.MentalState.Select(m => new MentalState(m)).ToList();
            Gender = npcData.Gender;
            CurrentThinking = npcData.CurrentThinking;
        }

        public void InitRelationships(NpcData npcData)
        {
            Relationships = npcData.Relationships.Select(r => new NpcRelationship(r)).ToList();
        }
        
        private int GetRelationshipWith(Npc npc)
        {
            for (int i = 0; i < Relationships.Count; i++)
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
            for (int i = 0; i < Relationships.Count; i++)
            {
                if (Relationships[i].Npc == npc)
                {
                    Relationships[i].SetNewAmount(amount);;
                }
            }
        }
    }
}