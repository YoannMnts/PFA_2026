using System;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public struct EventConsequence
    {
        [field : SerializeReference, SubclassSelector]
        public IOperand LeftOperand { get; private set; }
        
        [field : SerializeField]
        public EMathOperator Operator { get; private set; }
        
        [field : SerializeField]
        public int RightOperand { get; private set; }
    }
}