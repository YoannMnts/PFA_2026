using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Naussilus.Gameplay
{
    public class FillCategory : PhaseCompletionSource<bool>
    {
        public RoomAction CurrentAction { get; private set; }

        public Category[] Categories => CurrentAction.Categories;
        
        public RoomNpcSlot[] NpcSlots { get; private set; }
        public FillCategory(RoomAction action, RoomNpcSlot[] currentNpcSlots)
        {
            CurrentAction = action;
            NpcSlots = currentNpcSlots;
        }
    }
}