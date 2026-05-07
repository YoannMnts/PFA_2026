using System;
using System.Linq;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.VisualNovels.EventDatas;

namespace Naussilus.Core
{
    public struct Incident : IComparable<Incident>
    {
        public string Name { get; private set; }
        
        public Npc[] Npcs { get; private set; }
        
        public int Priority { get; private set; }
        
        public int DayCheck { get; private set; }
        
        public ConditionalEffect[] Dependencies { get; private set; }
        
        public Dialogue FirstDialogue { get; private set; }

        public Incident(EventData data)
        {
            Name = data.Name;
            Npcs = data.Npcs?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            Priority = data.Priority;
            DayCheck = data.DayCheck;
            Dependencies = data.Dependencies?.Select(d => new ConditionalEffect(d)).ToArray();
            FirstDialogue = new Dialogue(data.FirstDialogue);
        }

        public int CompareTo(Incident other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }
}