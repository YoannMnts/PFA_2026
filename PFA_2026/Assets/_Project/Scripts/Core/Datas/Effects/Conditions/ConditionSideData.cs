using System;
using UnityEngine;

namespace Naussilus.Core.Conditions
{
    [Serializable]
    public class ConditionSideData
    {
        [field : SerializeField]
        public bool IsCurrentNpc { get; private set; }
        
        [field: SerializeReference]
        public INpcSelectorData Subject { get; private set; }
        
        [field: SerializeReference]
        public IConditionEffectValueData Stat { get; private set; }
        
        [field : SerializeField]
        public bool UseEnumerationNpc { get; private set; }
        
        [field : SerializeField]
        public bool UseRelationshipNpcToReturn { get; private set; }
    }
}