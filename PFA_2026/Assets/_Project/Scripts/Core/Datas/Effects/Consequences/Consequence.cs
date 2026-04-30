using System;
using Naussilus.Core.Operators;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Naussilus.Core.Consequences
{
    [Serializable]
    public class Consequence
    {
        [field : SerializeField]
        public bool IsGameLost { get; private set; }
        
        [field : SerializeField]
        public ConsequenceSide ConsequenceSide { get; private set; }
        
        [field: SerializeField]
        public ArithmeticOperator ArithmeticOperator { get; private set; }
        
        [field: SerializeField]
        public int Amount { get; private set; }
        
        [field: SerializeField, TextArea]
        public string Text { get; private set; }
    }
}