using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Rooms
{
    public class ResumePhaseForAction : PhaseCompletionSource<bool>
    {
        public RoomAction CurrentRoomAction { get; private set; }
        
        public ResumePhaseForAction(RoomAction roomAction)
        {
            CurrentRoomAction = roomAction;
        }
    }
}