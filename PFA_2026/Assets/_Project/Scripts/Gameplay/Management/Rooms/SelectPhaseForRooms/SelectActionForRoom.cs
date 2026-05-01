using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core.Managements;
using Naussilus.Core.Managements.ActionDatas;
using UnityEngine;

namespace _Project.Scripts.Rooms
{
    public class SelectActionForRoom : PhaseCompletionSource<int>
    {
        public RoomData CurrentRoom { get; private set; }
        
        public ActionData[] Choices { get; private set; }
        
        public SelectActionForRoom(RoomData room)
        {
            CurrentRoom = room;
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