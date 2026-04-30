using System;
using UnityEngine;

namespace Naussilus.Core.Consequences
{
    [Serializable]
    public class ConsequenceSide
    {
        [field : SerializeField]
        public bool IsCurrentNpc { get; private set; }
        
        [field: SerializeReference]
        public INpcSelector Subject { get; private set; }
        
        [field: SerializeReference]
        public IConsequenceEffectValue Stat { get; private set; }
        
        [field : SerializeField]
        public bool UseEnumerationNpc { get; private set; }
        
        [field : SerializeField]
        public bool UseRelationshipNpcToReturn { get; private set; }
    }
}