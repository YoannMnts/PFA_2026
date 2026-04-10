using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core.Managements.RoomDatas;
using Naussilus.Core.Managements.RoomDatas.ActionDatas;
using UnityEngine;

namespace _Project.Scripts
{
    public class SelectActionForRoom : PhaseCompletionSource<int>
    {
        public Room CurrentRoom { get; private set; }
        public RoomData CurrentRoomData => CurrentRoom.RoomData;
        
        public ActionData[] Choices { get; private set; }
        
        public SelectActionForRoom(Room room)
        {
            CurrentRoom = room;
        }

        protected override Awaitable Initialize(CancellationToken token)
        {
            Choices = CurrentRoomData.Actions;
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            Choices = null;
            return base.Dispose(token);
        }
    }
}