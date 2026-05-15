using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Naussilus.Gameplay
{
    public class CurrentlyInAction : PhaseCompletionSource<bool>
    {
        public Room Room {get; private set;}
        public RoomAction CurrentAction => Room.CurrentAction;

        public CurrentlyInAction(Room room)
        {
            Room = room;
        }
    }
}