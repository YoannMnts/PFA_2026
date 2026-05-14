using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;

public class SelectRoomForShip : PhaseCompletionSource<Room>
{
    public Room[] CurrentRooms { get; private set; }

    public ActionPoint CurrentActionPoint { get; private set; }
    
    public ManagementPhase CurrentPhase { get; private set; }

    public SelectRoomForShip(ActionPoint actionPoint, ManagementPhase phase)
    {
        CurrentActionPoint = actionPoint;
        CurrentPhase = phase;
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