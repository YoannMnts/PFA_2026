using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class NpcMentalState : IConditionValue, IConsequenceValue
    {
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }

        [field: SerializeField]
        public MentalState Gauge { get; private set; }
        
        public void SetNewAmount(int amount)
        {
            Amount = amount;
        }
    }
}