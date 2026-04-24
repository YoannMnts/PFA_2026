using System;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Managements.RoomDatas.ActionDatas.Effects.ActionsEffects
{
    [Serializable]
    public struct ActionEffect
    {
        [field : SerializeField, Range(0, 10)]
        public int CategoryIndex { get; private set; }
        
        [field : SerializeField]
        public NpcData NpcData { get; private set; }
        
        [field : SerializeField]
        public ConditionalEffect[] Effects { get; private set; }
        
        [field : SerializeField]
        public Vector3 Position { get; private set; }
        
        [field : SerializeField]
        public Sprite ActionSprite { get; private set; } 
    }
}