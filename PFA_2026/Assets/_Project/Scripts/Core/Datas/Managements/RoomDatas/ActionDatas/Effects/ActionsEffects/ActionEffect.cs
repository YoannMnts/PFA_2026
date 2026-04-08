using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core.Managements.RoomDatas.ActionDatas.Effects.ActionsEffects
{
    [Serializable]
    public struct ActionEffect
    {
        [field: SerializeField]
        public ComplexCondition[] Conditions { get; private set; }
        
        [field: SerializeField]
        public EventConsequence[] Consequences { get; private set; }
    }
}