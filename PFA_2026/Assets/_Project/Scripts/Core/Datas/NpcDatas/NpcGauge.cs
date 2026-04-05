using System;
using UnityEngine;

namespace Naussilus.Core.Datas.NpcDatas
{
    [Serializable]
    public struct NpcGauge
    {
        [field: SerializeField, Range(0,20)]
        public int Happiness { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Confidence { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Integration { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Relaxation { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Health { get; private set; }
    }
}