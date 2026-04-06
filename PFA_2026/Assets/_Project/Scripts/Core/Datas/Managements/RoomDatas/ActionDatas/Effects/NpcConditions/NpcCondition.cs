using System;
using Naussilus.Core.Datas.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    [Serializable]
    public struct NpcCondition
    {
        [field: SerializeField]
        public NpcData Npc { get; private set; }
        
        [field: SerializeField]
        public int CategoryIndex { get; private set; }
    }
}