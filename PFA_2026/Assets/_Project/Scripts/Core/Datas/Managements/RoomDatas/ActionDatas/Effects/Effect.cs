using Naussilus.Core.Scripts.Effects.ActionEffects;
using Naussilus.Core.Scripts.Effects.NpcConditions;
using UnityEngine;

namespace Naussilus.Core.Scripts.Effects
{
    public struct Effect
    {
        [field: SerializeField]
        public NpcCondition NpcCondition { get; private set; }
        
        [field: SerializeField]
        public IActionsEffect[] ActionsEffects { get; private set; }
    }
}