using System;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public class Condition
    {
        [field: SerializeReference]
        public IConditionValue Left { get; private set; }
        
        [field: SerializeField]
        public ComparisonOperator ComparisonOperator { get; private set; }
        
        [field: SerializeReference]
        public IConditionValue Right { get; private set; }
    }
}