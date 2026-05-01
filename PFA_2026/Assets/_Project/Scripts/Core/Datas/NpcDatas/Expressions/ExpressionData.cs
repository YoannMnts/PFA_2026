using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [CreateAssetMenu(menuName = "Naussilus/NpcDatas/Expression")]
    public class ExpressionData : ScriptableObject
    {
        [field: SerializeField]
        public ExpressionValueData[] Expressions { get; private set; }
    }
}