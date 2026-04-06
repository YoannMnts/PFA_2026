using System;
using Naussilus.Core.Datas.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    [Serializable]
    public struct Category
    {
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public int Quantity { get; private set; }
        
        [field: SerializeField]
        public NpcData[] ProhibitedNpc { get; private set; }
        
        [field: SerializeField]
        public NpcData[] ObligateNpc { get; private set; }
    }
}