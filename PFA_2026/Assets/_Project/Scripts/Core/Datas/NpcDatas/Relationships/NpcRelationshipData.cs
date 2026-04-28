using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class NpcRelationshipData : IConditionValue, IConsequenceValue
    {
        [field : SerializeField]
        public bool UseCurrentNpc { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
    
        [field: SerializeReference]
        public INpcSelector Npc { get; private set; }
        
        public void SetNewAmount(int amount) => Amount = amount;
    
    }
}