using Naussilus.Core.Scripts.Conditions;
using Naussilus.Core.Scripts.Operators;
using UnityEngine;

namespace Naussilus.Core.Scripts.Effects.ActionEffects
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class BaseEffectData : ScriptableObject, IActionsEffect
    {
        [field: SerializeField]
        public Operand LeftOperand { get; private set; } 
        
        [field: SerializeField]
        public EMathOperator Operator { get; private set; }
        
        [field: SerializeField]
        public Operand RightOperand { get; private set; }
        
        [field: SerializeField]
        public ActionTarget StatTarget { get; private set; }
    }
}