using Naussilus.Core.Datas.EStats;
using UnityEngine;

namespace Naussilus.Core.DatasOperators
{
    public struct Operand
    {
        [field : SerializeField]
        public int[] Amount { get; private set; }
        
        [field : SerializeField]
        public Stat[] Stat { get; private set; }
        
        [field : SerializeField]
        public RelationshipOperand[] NpcRelationship { get; private set; }
    }
}