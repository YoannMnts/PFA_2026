using Naussilus.Core.Managers.Npcs;

namespace Naussilus.Core
{
    public struct NpcCondition
    {
        public Npc Npc { get; private set; }
        
        public int CategoryIndex { get; private set; }
    }
}