using System;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Effects.ActionsEffects;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Effects.NpcConditions;
using UnityEngine;

namespace Naussilus.Core.Managements.RoomDatas.ActionDatas.Effects
{
    [Serializable]
    public struct Effect
    {
        [field: SerializeField]
        public NpcCondition NpcCondition { get; private set; }
        
        [field: SerializeField]
        public ActionEffect[] ActionsEffects { get; private set; }
    }
}