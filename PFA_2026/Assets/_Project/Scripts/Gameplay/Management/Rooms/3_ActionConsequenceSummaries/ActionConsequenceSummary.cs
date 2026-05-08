using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using UnityEngine;

namespace Rooms
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