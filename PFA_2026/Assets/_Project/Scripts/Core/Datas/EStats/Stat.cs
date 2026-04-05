using UnityEngine;

namespace Naussilus.Core.Scripts.EStats
{
    public struct Stat
    {
        [field: SerializeField]
        public EStat[] Stats { get; private set; }
        
        [field: SerializeField]
        public EGauge[] Gauges { get; private set; }
    }
}