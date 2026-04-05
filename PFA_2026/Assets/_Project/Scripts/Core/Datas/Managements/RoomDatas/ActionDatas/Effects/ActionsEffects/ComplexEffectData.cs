using Naussilus.Core.Datas.Conditions;
using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class ComplexEffectData : ScriptableObject, IActionsEffect
    {
        [field: SerializeField]
        public ComplexCondition[] Conditions { get; private set; }
        
        [field: SerializeField]
        public BaseEffectData Effect { get; private set; }
    }
}