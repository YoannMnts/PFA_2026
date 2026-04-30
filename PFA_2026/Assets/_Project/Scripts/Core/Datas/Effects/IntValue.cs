using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core
{
    [Serializable]
    public class IntValue : IConditionEffectValue, INpcStat
    {
        [field: SerializeField, Range(0, 20)]
        public int Amount { get; private set; }
    }
}