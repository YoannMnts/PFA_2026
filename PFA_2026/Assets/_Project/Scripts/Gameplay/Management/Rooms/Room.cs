using Helteix.Tools.Phases;
using Naussilus.Core.Managements.RoomDatas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Rooms
{
    public class Room : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] 
        public RoomData RoomData { get; private set; }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            SelectActionForRoom selectActionForRoom = new SelectActionForRoom(this);
            selectActionForRoom.RunAndForget();
        }
    }
}