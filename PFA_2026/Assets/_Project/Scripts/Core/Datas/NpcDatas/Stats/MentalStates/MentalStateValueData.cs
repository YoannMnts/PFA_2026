using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class MentalStateValueData : IConditionEffectValueData, IConsequenceEffectValueData
    {
        [field: SerializeField]
        public MentalStateData Stat { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Amount { get; private set; }
    }
}