using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class BehaviorValue: IConditionEffectValue, IConsequenceEffectValue
    {
        [field: SerializeField]
        public BehaviorData Stat { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
    }
}