using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core
{
    [Serializable]
    public class ConditionalEffect
    {
        [field: SerializeField] 
        public bool IsEnumeration { get; private set; }
        
        [field: SerializeReference] 
        public INpcSelector CurrentNpcTarget { get; private set; }
        
        [field: SerializeField]
        public Condition[] Conditions { get; private set; }
        
        [field: SerializeField]
        public Consequence[] Consequences { get; private set; }
    }
}