using System;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public class IntValue : IConditionValue
    {
        [field: SerializeField]
        public int Amount { get; private set; }
    }
}