using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managements;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;

namespace Rooms
{
    public class MonoRoom : MonoBehaviour
    {
        [field: SerializeField] 
        public RoomData RoomData { get; private set; }

        private Room Room => RoomManager.TryGetRoom(RoomData.GUID);
        
        public void OnClick()
        {
            SelectActionForRoom selectActionForRoom = new SelectActionForRoom(Room);
            selectActionForRoom.RunAndForget();
        }
    }
}