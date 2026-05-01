using System;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class ExpressionValueData
    {
        [field: SerializeField]
        public Sprite Sprite { get; private set; }
        
        [field: SerializeField]
        public NpcData Npc { get; private set; }
    }
}