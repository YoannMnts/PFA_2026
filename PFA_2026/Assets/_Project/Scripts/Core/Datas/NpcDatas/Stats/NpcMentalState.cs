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
        public MentalState MentalState { get; private set; }
    
        public void SetNewAmount(int amount) => Amount = amount;
    
        public NpcMentalState Clone() => new NpcMentalState(Amount, MentalState);
    
        public NpcMentalState() { }
        public NpcMentalState(int amount, MentalState mentalState)
        {
            Amount = amount;
            MentalState = mentalState;
        }
    }
}