using System;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class NpcRelationship : IConditionValue
    {
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
        
        [field: SerializeReference]
        public NpcData Npc { get; private set; }
    }
}