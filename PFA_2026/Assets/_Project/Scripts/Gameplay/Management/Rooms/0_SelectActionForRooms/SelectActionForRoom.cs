using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SelectActionForRoom : PhaseCompletionSource<bool>
    {
        public Room CurrentRoom { get; private set; }
        
        public RoomAction[] Choices { get; private set; }
        
        public RoomAction CurrentAction { get; private set; }
        
        public RoomNpcSlot[] NpcSlots { get; private set; }
        
        public ActionPoint CurrentActionPoint { get; private set; }
        
        public MonoCineCamera CurrentCineCamera { get; private set; }
        
        public SelectActionForRoom(Room room, ActionPoint actionPoint, RoomNpcSlot[] npcSlots, MonoCineCamera cineCamera)
        {
            CurrentRoom = room;
            CurrentActionPoint = actionPoint;
            NpcSlots = npcSlots;
            CurrentCineCamera = cineCamera;
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