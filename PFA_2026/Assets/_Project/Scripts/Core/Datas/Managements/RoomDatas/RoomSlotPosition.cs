using UnityEngine;

namespace Naussilus.Core.Managements
{
    [CreateAssetMenu(fileName = "RoomSlot", menuName = "Naussilus/Management/RoomData", order = 0)]
    public class RoomSlotPosition : ScriptableObject
    {
        [field: SerializeField]
        public Vector3 Position { get; private set; }
    }
}