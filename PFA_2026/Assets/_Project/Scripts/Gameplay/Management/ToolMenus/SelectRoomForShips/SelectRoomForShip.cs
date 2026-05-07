using System.Threading;
using DefaultNamespace;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;

public class SelectRoomForShip : PhaseCompletionSource<Room>
{
    public Room[] CurrentRooms { get; private set; }

    public ActionPoint CurrentActionPoint { get; private set; }

    public SelectRoomForShip(ActionPoint actionPoint)
    {
        CurrentActionPoint = actionPoint;
    }
    
    protected override Awaitable Initialize(CancellationToken token)
    {
        CurrentRooms = RoomManager.GetAllRooms();
        return base.Initialize(token);
    }

    protected override Awaitable Dispose(CancellationToken token)
    {
        CurrentRooms = null;
        return base.Dispose(token);
    }
}