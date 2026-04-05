using Naussilus.Core.Datas.Conditions;
using Naussilus.Core.DatasOperators;
using UnityEngine;

namespace Naussilus.Core.Datas.Managements
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