using System;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public class ConditionSide
    {
        [field : SerializeField]
        public bool IsCurrentNpc { get; private set; }
        
        [field: SerializeReference]
        public INpcSelector Subject { get; private set; }
        
        [field: SerializeReference]
        public IConditionEffectValue Stat { get; private set; }
        
        [field : SerializeField]
        public bool UseEnumerationNpc { get; private set; }
        
        [field : SerializeField]
        public bool UseRelationshipNpcToReturn { get; private set; }
    }
}