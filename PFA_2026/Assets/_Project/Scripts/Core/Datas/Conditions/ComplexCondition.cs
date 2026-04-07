using System;
using Naussilus.Core.DatasOperators;
using UnityEngine;

namespace Naussilus.Core.Datas.Conditions
{
    [Serializable]
    public struct ComplexCondition
    {
        [field: SerializeReference]
        public IOperand LeftOperand { get; private set; }
        
        [field: SerializeField]
        public EComparator Comparator { get; private set; }
        
        [field: SerializeField]
        public IOperand RightOperand { get; private set; }
        
        [field: SerializeReference]
        public ActionTarget StatTarget { get; private set; }
    }
}