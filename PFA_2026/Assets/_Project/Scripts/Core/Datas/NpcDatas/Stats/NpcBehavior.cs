using System;
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
        public Behavior Stat { get; private set; }
    }
}