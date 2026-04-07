using System;
using Naussilus.Core.DatasOperators;
using UnityEngine;

namespace Naussilus.Core.Datas.EStats
{
    [Serializable]
    public struct Stat : IOperand
    {
        [field: SerializeField]
        public EStat[] Stats { get; private set; }
        
        [field: SerializeField]
        public EGauge[] Gauges { get; private set; }
    }
}