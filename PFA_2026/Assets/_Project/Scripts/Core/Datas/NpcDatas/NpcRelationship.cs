using System;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [Serializable]
    public struct NpcRelationship
    {
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
        
        [field: SerializeField]
        public NpcData Npc { get; private set; }
    }
}