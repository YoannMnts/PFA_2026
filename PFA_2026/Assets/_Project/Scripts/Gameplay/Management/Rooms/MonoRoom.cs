using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managements;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rooms
{
    public class MonoRoom : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] 
        public RoomData RoomData { get; private set; }

        private Room Room => RoomManager.TryGetRoom(RoomData.GUID);
        
        public void OnPointerClick(PointerEventData eventData)
        {
            SelectActionForRoom selectActionForRoom = new SelectActionForRoom(Room);
            selectActionForRoom.RunAndForget();
        }
    }
}