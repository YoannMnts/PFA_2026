using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Rooms
{
    public class SelectNpcsForAction : PhaseCompletionSource<bool>
    {
        public RoomAction CurrentAction { get; private set; }

        public Category[] Categories => CurrentAction.Categories;
        
        public SelectNpcsForAction(RoomAction action)
        {
            CurrentAction = action;
        }
    }
}