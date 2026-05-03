using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;

public class SelectRoomForShip : PhaseCompletionSource<Room>
{
    public Room[] CurrentRooms { get; private set; }

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