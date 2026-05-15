using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class CurrentlyInAction : PhaseCompletionSource<bool>
    {
        public Room Room {get; private set;}
        public RoomAction CurrentAction => Room.CurrentAction;
        
        public MonoCineCamera CurrentCineCamera  {get; private set;}

        public CurrentlyInAction(Room room, MonoCineCamera cineCamera)
        {
            Room = room;
            CurrentCineCamera = cineCamera;
        }
    }
}