using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Managements
{
    [CreateAssetMenu(fileName = "CategorySlot", menuName = "Naussilus/Management/CategorySlot", order = 0)]
    public class RoomSlotPositionData : ScriptableObject
    {
        [field: SerializeField]
        public Vector3 Position { get; private set; }
        
        [field: SerializeField]
        public ExpressionData Expression { get; private set; }
    }
}