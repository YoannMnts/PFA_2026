using Naussilus.Core.Scripts.Operators;
using UnityEngine;

namespace Naussilus.Core.Scripts.VisualNovels.EventDatas.DialogueDatas.Answers.EventConsequences
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