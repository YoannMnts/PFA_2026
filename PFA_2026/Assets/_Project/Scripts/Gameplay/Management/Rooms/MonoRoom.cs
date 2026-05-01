using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managements;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Rooms
{
    public class MonoRoom : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] 
        public RoomData RoomData { get; private set; }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var room = RoomManager.TryGetRoom(RoomData.GUID);
            SelectActionForRoom selectActionForRoom = new SelectActionForRoom(room);
            selectActionForRoom.RunAndForget();
        }
    }
}