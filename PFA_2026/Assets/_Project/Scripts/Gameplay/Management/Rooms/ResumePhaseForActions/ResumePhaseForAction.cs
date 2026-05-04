using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using UnityEngine;

namespace Rooms
{
    public class ResumePhaseForAction : PhaseCompletionSource<bool>
    {
        public RoomAction CurrentRoomAction { get; private set; }
        public SelectNpcsForAction SelectNpcsForAction { get; private set; }
        
        public ResumePhaseForAction(RoomAction roomAction)
        {
            CurrentRoomAction = roomAction;
        }
    }
}