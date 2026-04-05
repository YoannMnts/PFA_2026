using Naussilus.Core.DatasOperators;
using UnityEngine;

namespace Naussilus.Core.Datas.VisualNovels
{
    public struct EventConsequence
    {
        [field : SerializeField]
        public Operand LeftOperand { get; private set; }
        
        [field : SerializeField]
        public EMathOperator Operator { get; private set; }
        
        [field : SerializeField]
        public int RightOperand { get; private set; }
    }
}