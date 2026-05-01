using System;
using UnityEngine;

namespace Naussilus.Core.Consequences
{
    [Serializable]
    public class ConsequenceSideData
    {
        [field : SerializeField]
        public bool IsCurrentNpc { get; private set; }
        
        [field: SerializeReference]
        public INpcSelectorData Subject { get; private set; }
        
        [field: SerializeReference]
        public IConsequenceEffectValueData Stat { get; private set; }
        
        [field : SerializeField]
        public bool UseEnumerationNpc { get; private set; }
        
        [field : SerializeField]
        public bool UseRelationshipNpcToReturn { get; private set; }
    }
}