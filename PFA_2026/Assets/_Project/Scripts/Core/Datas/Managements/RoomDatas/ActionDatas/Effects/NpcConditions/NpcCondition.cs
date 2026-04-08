using System;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Managements.RoomDatas.ActionDatas.Effects.NpcConditions
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