using System;
using Naussilus.Core.Operators;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Naussilus.Core.Consequences
{
    [Serializable]
    public class ConsequenceData
    {
        [field : SerializeField]
        public bool IsGameLost { get; private set; }
        
        [field : SerializeField]
        public ConsequenceSideData ConsequenceSide { get; private set; }
        
        [field: SerializeField]
        public ArithmeticOperator ArithmeticOperator { get; private set; }
        
        [field: SerializeField]
        public int Amount { get; private set; }
        
        [field: SerializeField, TextArea]
        public string[] Text { get; private set; }
    }
}