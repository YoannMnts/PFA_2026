using Naussilus.Core.Scripts.EStats;
using UnityEngine;

namespace Naussilus.Core.Scripts.Operators
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