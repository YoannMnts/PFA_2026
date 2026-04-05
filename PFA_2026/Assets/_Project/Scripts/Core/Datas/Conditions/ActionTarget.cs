using Naussilus.Core.Datas.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Datas.Conditions
{
    public struct ActionTarget
    {
        [field: SerializeField]
        public NpcData[] Npc { get; private set; }
        
        [field: SerializeField]
        public int[] CategoryIndex { get; private set; }
    }
}