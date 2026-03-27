using System;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [Serializable]
    public struct Relationship
    {
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
        
        [field: SerializeField]
        public NpcData Npc1 { get; private set; }
        
        [field: SerializeField]
        public NpcData Npc2 { get; private set; }
    }
}