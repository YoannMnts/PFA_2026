using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class NpcBehavior : IConditionValue, IConsequenceValue
    {
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }

        [field: SerializeField]
        public Behavior Behavior { get; private set; }
    
        public void SetNewAmount(int amount) => Amount = amount;
    
        public NpcBehavior Clone() => new NpcBehavior(Amount, Behavior);
        
        public NpcBehavior() { }
        public NpcBehavior(int amount, Behavior behavior)
        {
            Amount = amount;
            Behavior = behavior;
        }
    }
}