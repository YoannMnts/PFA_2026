using System;
using Naussilus.Core.Datas.Conditions;
using Naussilus.Core.DatasOperators;
using UnityEngine;

namespace Naussilus.Core.Datas.EStats
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