using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managements.ActionDatas;
using UnityEngine;

namespace _Project.Scripts.Rooms
{
    public class SelectNpcsForAction : PhaseCompletionSource<bool>
    {
        public Action CurrentAction { get; private set; }
        
        public Category[] Categories { get; private set; }
        
        public SelectNpcsForAction(Action action)
        {
            CurrentAction = action;
        }

        protected override Awaitable Initialize(CancellationToken token)
        {
            Categories = CurrentAction.Categories;
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            Categories = null;
            return base.Dispose(token);
        }
    }
}