using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Rooms
{
    public class CurrentlyInAction : PhaseCompletionSource<bool>
    {
        public RoomAction CurrentAction { get; private set; }

        public CurrentlyInAction(RoomAction action)
        {
            CurrentAction = action;
        }
    }
}