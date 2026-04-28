using Helteix.Tools.Phases;
using UnityEngine;

namespace _Project.Scripts
{
    public class MenuTool : MonoBehaviour
    {
        public void OnShipClicked()
        {
            var selectRoomForShip = new SelectRoomForShip();
            selectRoomForShip.RunAndForget();
        }

        public void OnLogClicked()
        {
            
        }

        public void OnOptionsClicked()
        {
            
        }
    }
}