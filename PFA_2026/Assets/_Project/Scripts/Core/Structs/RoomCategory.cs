using System;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [Serializable]
    public struct RoomCategory
    {
        [field : SerializeField]
        public int NpcQuantity { get; private set; }
        
        [field: SerializeField]
        public NpcData[] NeededNpc { get; private set; }
        
        [field: SerializeField]
        public NpcData[] ProhibitedNpc { get; private set; }
    }
}