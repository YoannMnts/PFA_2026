using System;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Consequences
{
    [Serializable]
    public class Consequence
    {
        [field : SerializeField]
        public bool IsCurrentNpc { get; private set; }
        
        [field: SerializeReference]
        public INpcSelector Subject { get; private set; }
        
        [field: SerializeReference]
        public IConsequenceValue IntTarget { get; private set; }
        
        [field: SerializeField]
        public ArithmeticOperator ArithmeticOperator { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
    }
}