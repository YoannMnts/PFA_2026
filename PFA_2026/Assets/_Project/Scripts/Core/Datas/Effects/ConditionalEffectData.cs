using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core
{
    [Serializable]
    public class ConditionalEffectData
    {
        [field: SerializeField] 
        public bool IsEnumeration { get; private set; }
        
        [field: SerializeReference] 
        public INpcSelectorData CurrentNpcTarget { get; private set; }
        
        [field: SerializeField]
        public ConditionData[] Conditions { get; private set; }
        
        [field: SerializeField]
        public ConsequenceData[] Consequences { get; private set; }
    }
}