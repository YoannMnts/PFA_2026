using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    [Serializable]
    public struct IntOperand : IOperand
    {
        [field: SerializeField]
        public int Amount { get; private set; }

        [field: SerializeReference, SubclassSelector, HideInInspector]
        public ITarget Target { get; private set; }
    }
}