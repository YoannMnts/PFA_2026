using Helteix.Tools.Phases;
using Naussilus.Core;

namespace _Project.Scripts.Rooms
{
    public class SelectNpcsForAction : PhaseCompletionSource<bool>
    {
        public Action CurrentAction { get; private set; }

        public Category[] Categories => CurrentAction.Categories;
        
        public SelectNpcsForAction(Action action)
        {
            CurrentAction = action;
        }
    }
}