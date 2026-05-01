using System;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Managements.ActionDatas
{
    [Serializable]
    public class NpcConditionData
    {
        [field: SerializeField]
        public NpcData Npc { get; private set; }
        
        [field: SerializeField]
        public int CategoryIndex { get; private set; }
    }
}