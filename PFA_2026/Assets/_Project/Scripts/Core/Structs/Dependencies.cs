using System;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [Serializable]
    public struct Dependencies
    {
        [field: SerializeField]
        public Gauge Gauge { get; private set; }
        
        [field: SerializeField]
        public int Day { get; private set; }
        
        [field: SerializeField]
        public Relationship Relationship { get; private set; }
    }
}