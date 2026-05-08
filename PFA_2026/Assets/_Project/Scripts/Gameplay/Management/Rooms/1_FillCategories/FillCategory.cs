using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Rooms
{
    public class FillCategory : PhaseCompletionSource<bool>
    {
        public RoomAction CurrentAction { get; private set; }

        public Category[] Categories => CurrentAction.Categories;
        
        public FillCategory(RoomAction action)
        {
            CurrentAction = action;
        }
    }
}