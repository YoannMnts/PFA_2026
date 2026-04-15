using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    [Serializable]
    public class Stat : IOperand
    {
        [field: SerializeField]
        public Behavior Behavior { get; private set; }

        [field: SerializeReference]
        public ITarget Target { get; private set; }
    }
    
    [Serializable]
    public class Gauge : IOperand
    {
        [field: SerializeField]
        public MentalState MentalState { get; private set; }

        [field: SerializeReference]
        public ITarget Target { get; private set; }
    }
}