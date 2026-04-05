using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    public struct Effect
    {
        [field: SerializeField]
        public NpcCondition NpcCondition { get; private set; }
        
        [field: SerializeField]
        public IActionsEffect[] ActionsEffects { get; private set; }
    }
}