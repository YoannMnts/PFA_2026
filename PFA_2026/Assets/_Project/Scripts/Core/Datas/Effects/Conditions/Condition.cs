using System;
using Naussilus.Core.Consequences;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public class Condition
    {
        [field : SerializeField]
        public bool IsCurrentNpc { get; private set; }
        
        [field: SerializeReference]
        public INpcSelector LeftSubject { get; private set; }
        
        [field: SerializeReference]
        public IConditionValue Left { get; private set; }
        
        [field: SerializeField]
        public ComparisonOperator ComparisonOperator { get; private set; }
        
        
        [field: SerializeReference]
        public INpcSelector RightSubject { get; private set; }
        
        [field: SerializeReference]
        public IConditionValue Right { get; private set; }
    }
}