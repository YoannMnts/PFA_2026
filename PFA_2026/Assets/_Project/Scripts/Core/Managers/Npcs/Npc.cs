using System.Linq;
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
            Behaviors = npcData.Behavior.ToArray();
            MentalStates = npcData.MentalState.ToArray();
            Gender = npcData.Gender;
            Relationships = npcData.Relationships.ToArray();
            CurrentThinking = npcData.CurrentThinking;
        }
    }
}