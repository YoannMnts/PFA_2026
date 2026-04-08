using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    [Serializable]
    public struct RelationshipOperand : IOperand
    {
        [field: SerializeField]
        public Relationship Npc1 { get; private set; }
        
        [field: SerializeField]
        public Relationship Npc2 { get; private set; }

        [field: SerializeReference, SubclassSelector,HideInInspector]
        public ITarget Target { get; private set; }
    }
}