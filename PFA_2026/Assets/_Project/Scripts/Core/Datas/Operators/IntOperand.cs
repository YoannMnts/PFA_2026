using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    [Serializable]
    public class IntOperand : IOperand
    {
        [field: SerializeField, Range(0, 20)]
        public int Amount { get; private set; }

        [field: SerializeReference, HideInInspector]
        public ITarget Target { get; private set; }
    }
}