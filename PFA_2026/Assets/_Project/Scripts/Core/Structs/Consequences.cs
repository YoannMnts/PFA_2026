using System;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [Serializable]
    public struct Consequences
    {
        [field: SerializeField]
        public int Gauge { get; private set; }
        
        [field: SerializeField]
        public int Stats { get; private set; }
        
        [field: SerializeField]
        public Relationship Relationship { get; private set; }
    }
}