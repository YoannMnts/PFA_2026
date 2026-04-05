using UnityEngine;

namespace Naussilus.Core.Scripts.Effects.NpcConditions
{
    public struct NpcCondition
    {
        [field: SerializeField]
        public NpcData Npc { get; private set; }
        
        [field: SerializeField]
        public int CategoryIndex { get; private set; }
    }
}