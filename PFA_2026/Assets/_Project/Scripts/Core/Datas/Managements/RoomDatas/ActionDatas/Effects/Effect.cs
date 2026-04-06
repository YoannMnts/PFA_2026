using System;
using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    [Serializable]
    public struct Effect
    {
        [field: SerializeField]
        public NpcCondition NpcCondition { get; private set; }
        
        [field: SerializeField]
        public ActionsEffect[] ActionsEffects { get; private set; }
    }
}