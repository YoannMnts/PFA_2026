using System;
using System.Linq;
using Naussilus.Core.Managers;
using Naussilus.Core.NpcDatas;
using Naussilus.Gameplay.Npcs;
using UnityEngine;

namespace Naussilus.Core
{
    [Serializable]
    public struct ConditionalEffect
    {
        [field: SerializeField]
        public Condition[] Conditions { get; private set; }
        
        [field: SerializeField]
        public Consequence[] Consequences { get; private set; }
        
        public bool AreConditionsMet(Npc currentNpc) => Conditions.All(c => c.ComputeCondition(currentNpc));
    }
}