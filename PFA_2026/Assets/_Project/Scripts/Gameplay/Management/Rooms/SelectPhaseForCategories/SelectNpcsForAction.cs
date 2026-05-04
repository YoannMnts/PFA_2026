using System;
using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Rooms
{
    public class SelectNpcsForAction : PhaseCompletionSource<bool>
    {
        public event Action<RoomAction> OnActionApply;
        
        public RoomAction CurrentAction { get; private set; }

        public Category[] Categories => CurrentAction.Categories;
        
        public SelectNpcsForAction(RoomAction action)
        {
            CurrentAction = action;
        }

        public void Apply()
        {
            OnActionApply?.Invoke(CurrentAction);
        }
    }
}