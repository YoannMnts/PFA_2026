using Naussilus.Core.DatasOperators;
using UnityEngine;

namespace Naussilus.Core.Datas.Conditions
{
    public struct ComplexCondition
    {
        [field: SerializeField]
        public Operand LeftOperand { get; private set; }
        
        [field: SerializeField]
        public EComparator Comparator { get; private set; }
        
        [field: SerializeField]
        public Operand RightOperand { get; private set; }
        
        [field: SerializeField]
        public ActionTarget StatTarget { get; private set; }
    }
}