using System;
using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core;
using UnityEngine;

namespace Rooms
{
    public class SelectActionForRoom : PhaseCompletionSource<bool>
    {
        public Room CurrentRoom { get; private set; }
        
        public RoomAction[] Choices { get; private set; }
        
        public RoomAction CurrentAction { get; private set; }
        
        public ActionPoint CurrentActionPoint { get; private set; }
        public SelectActionForRoom(Room room, ActionPoint actionPoint)
        {
            CurrentRoom = room;
            CurrentActionPoint = actionPoint;
        }

        protected override Awaitable Initialize(CancellationToken token)
        {
            Choices = CurrentRoom.Actions;
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            Choices = null;
            return base.Dispose(token);
        }
    }
}