using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using Naussilus.Core.Operators;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class NpcRelationshipData : IConditionValue, IConsequenceValue
    {
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
    
        [field: SerializeReference]
        public INpcSelector Npc { get; private set; }

        public NpcRelationshipData() { }
        public NpcRelationshipData(int amount, INpcSelector npc)
        {
            Amount = amount;
            Npc = npc;
        }
        
        public void SetNewAmount(int amount) => Amount = amount;
    
    }
}