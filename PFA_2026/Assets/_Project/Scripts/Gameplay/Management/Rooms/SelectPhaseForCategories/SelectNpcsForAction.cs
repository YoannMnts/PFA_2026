using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core.Managements.RoomDatas.ActionDatas;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using UnityEngine;

namespace _Project.Scripts.Rooms
{
    public class SelectNpcsForAction : PhaseCompletionSource<bool>
    {
        public ActionData CurrentAction { get; private set; }
        
        public Category[] Categories { get; private set; }
        
        public SelectNpcsForAction(ActionData action)
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