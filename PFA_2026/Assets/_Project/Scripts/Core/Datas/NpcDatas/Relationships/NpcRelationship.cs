using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class NpcRelationship : IConditionValue, IConsequenceValue
    {
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
    
        [field: SerializeReference]
        public IRelationshipValue Npc { get; private set; }

        public NpcRelationship(int amount, IRelationshipValue npc)
        {
            Amount = amount;
            Npc = npc;
        }
        public NpcRelationship Clone() => new NpcRelationship(Amount, Npc);
    
        public void SetNewAmount(int amount) => Amount = amount;
    
    }
}