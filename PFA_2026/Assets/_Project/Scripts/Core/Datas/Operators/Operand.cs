using System;
using Naussilus.Core.Datas.EStats;
using UnityEngine;

namespace Naussilus.Core.DatasOperators
{
    [Serializable]
    public struct Operand
    {
        [field: SerializeField]
        public int[] Amount { get; private set; }
        
        [field: SerializeField]
        public Stat[] Stats { get; private set; }
        
        [field: SerializeField]
        public RelationshipOperandNpc[] NpcRelationship { get; private set; }
    }
}