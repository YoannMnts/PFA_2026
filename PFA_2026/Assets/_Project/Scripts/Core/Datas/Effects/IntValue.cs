using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core
{
    [Serializable]
    public class IntValue : IConditionValue
    {
        [field: SerializeField, Range(0, 20)]
        public int Amount { get; private set; }
    }
}