using System;
using Naussilus.Core.DatasOperators;
using UnityEngine;

namespace Naussilus.Core.Datas.Conditions
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
        
        [field: SerializeReference, SubclassSelector]
        public ITarget StatTarget { get; private set; }
    }
}