using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class NpcRelationshipData : IConditionEffectValueData, IConsequenceEffectValueData
    {
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
    
        [field: SerializeReference]
        public INpcSelectorData Npc { get; private set; }
        
        public void SetNewAmount(int amount) => Amount = amount;
    }
}