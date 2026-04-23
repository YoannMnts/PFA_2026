using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core
{
    [Serializable]
    public struct ConditionalEffect
    {
        [field: SerializeField]
        public Condition[] Conditions { get; private set; }
        
        [field: SerializeField]
        public Consequence[] Consequences { get; private set; }
    }
}