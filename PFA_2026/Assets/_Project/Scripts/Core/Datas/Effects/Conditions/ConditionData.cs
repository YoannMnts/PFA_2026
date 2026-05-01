using System;
using Naussilus.Core.Consequences;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public class ConditionData
    {
        [field: SerializeField] 
        public ConditionSideData LeftSide { get; private set; }

        [field: SerializeField]
        public ComparisonOperator ComparisonOperator { get; private set; }
        
        [field: SerializeField]
        public ConditionSideData RightSide { get; private set; }
    }
}