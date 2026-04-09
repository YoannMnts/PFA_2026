using System.Collections.Generic;
using Helteix.Tools.Phases;
using Helteix.Tools.UI;
using Naussilus.Core.Managements.RoomDatas;
using Naussilus.Core.Managements.RoomDatas.ActionDatas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
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