using System;
using Naussilus.Core.Consequences;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public class Condition
    {
        [field: SerializeField] 
        public ConditionSide LeftSide { get; private set; }

        [field: SerializeField]
        public ComparisonOperator ComparisonOperator { get; private set; }
        
        [field: SerializeField]
        public ConditionSide RightSide { get; private set; }
    }
}