using System;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public struct ComplexCondition
    {
        [field: SerializeReference, SubclassSelector]
        public IOperand LeftOperand { get; private set; }
        
        [field: SerializeField]
        public EComparator Comparator { get; private set; }
        
        [field: SerializeReference, SubclassSelector]
        public IOperand RightOperand { get; private set; }
    }
}