using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core
{
    [Serializable]
    public class IntValue : IConditionValue
    {
        [field: SerializeField]
        public int Amount { get; private set; }
    }
}