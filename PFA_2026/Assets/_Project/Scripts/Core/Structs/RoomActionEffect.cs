using System;
using Naussilus.Core.Scripts.Enums;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [Serializable]
    public struct RoomActionEffect
    {
        [field: SerializeField]
        public Enums.NpcStats NpcStat { get; private set; }
        
        [field: SerializeField]
        public Maths Operator { get; private set; }
        
        [field: SerializeField]
        public int Amount { get; private set; }
    }
}