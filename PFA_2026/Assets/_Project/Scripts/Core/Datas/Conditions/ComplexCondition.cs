using System;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public struct ComplexCondition
    {
        [field: SerializeReference]
        public IOperand LeftOperand { get; private set; }
        
        [field: SerializeField]
        public EComparator Comparator { get; private set; }
        
        [field: SerializeReference]
        public IOperand RightOperand { get; private set; }
    }
}