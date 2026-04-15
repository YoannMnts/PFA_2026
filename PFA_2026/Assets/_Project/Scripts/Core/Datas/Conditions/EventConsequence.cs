using System;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public struct EventConsequence
    {
        [field : SerializeReference]
        public INpcIntOperand Operand { get; private set; }
        
        [field : SerializeField]
        public EMathOperator Operator { get; private set; }
    }
}