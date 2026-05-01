using System;
using UnityEngine;

namespace Naussilus.Core.Managements.ActionDatas
{
    [Serializable]
    public struct EffectData
    {
        [field: SerializeField]
        public NpcConditionData NpcCondition { get; private set; }
        
        [field: SerializeField]
        public ActionEffectData[] ActionsEffects { get; private set; }
    }
}