using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    [Serializable]
    public struct RelationshipOperand : IOperand
    {
        [field: SerializeReference, SubclassSelector]
        public IRelationshipOperand Npc1 { get; private set; }
        
        [field: SerializeReference, SubclassSelector]
        public IRelationshipOperand Npc2 { get; private set; }

        [field: SerializeReference, SubclassSelector,HideInInspector]
        public ITarget Target { get; private set; }
    }
}