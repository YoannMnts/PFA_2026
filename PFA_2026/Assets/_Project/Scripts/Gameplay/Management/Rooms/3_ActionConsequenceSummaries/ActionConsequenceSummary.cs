using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Naussilus.Gameplay
{
    public class ActionConsequenceSummary : PhaseCompletionSource<bool>
    {
        public RoomAction CurrentRoomAction { get; private set; }
        
        public ActionConsequenceSummary(RoomAction roomAction)
        {
            CurrentRoomAction = roomAction;
        }
    }
}