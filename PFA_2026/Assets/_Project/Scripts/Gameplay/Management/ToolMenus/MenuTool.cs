using Helteix.Tools.Phases;
using UnityEngine;

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