using System;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core
{
    [Serializable]
    public class Consequence
    {
        [field: SerializeReference]
        public IConsequenceValue Target { get; private set; }
        
        [field: SerializeField]
        public ArithmeticOperator ArithmeticOperator { get; private set; }
        
        [field: SerializeField]
        public int Amount { get; private set; }
    }
}