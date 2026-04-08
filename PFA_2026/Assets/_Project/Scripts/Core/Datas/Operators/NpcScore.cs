using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    [Serializable]
    public struct Stat : IOperand
    {
        [field: SerializeField]
        public EStat EStat { get; private set; }

        [field: SerializeReference, SubclassSelector]
        public ITarget Target { get; private set; }
    }
    
    [Serializable]
    public struct Gauge : IOperand
    {
        [field: SerializeField]
        public EGauge EGauge { get; private set; }

        [field: SerializeReference, SubclassSelector]
        public ITarget Target { get; private set; }
    }
}