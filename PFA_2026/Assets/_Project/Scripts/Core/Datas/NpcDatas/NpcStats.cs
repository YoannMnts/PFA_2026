using System;
using UnityEngine;

namespace Naussilus.Core.Datas.NpcDatas
{
    [Serializable]
    public struct NpcStats
    {
        [field: SerializeField, Range(0,20)]
        public int Aggressiveness { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Compassion { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Sensitivity { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Expressiveness { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Tact { get; private set; }
        
        [field: SerializeField, Range(0,20)]
        public int Malleability { get; private set; }
    }
}