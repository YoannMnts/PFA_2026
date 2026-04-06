using System;
using Naussilus.Core.Datas.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Datas.Conditions
{
    [Serializable]
    public struct ActionTarget
    {
        [field: SerializeField]
        public NpcData[] Npc { get; private set; }
        
        [field: SerializeField]
        public int[] CategoryIndex { get; private set; }
        
        [field : SerializeField]
        public EGender[] Gender { get; private set; }
    }
}