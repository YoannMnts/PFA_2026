using UnityEngine;

namespace Naussilus.Core.Scripts.Conditions
{
    public struct ActionTarget
    {
        [field: SerializeField]
        public NpcData[] Npc { get; private set; }
        
        [field: SerializeField]
        public int[] CategoryIndex { get; private set; }
    }
}