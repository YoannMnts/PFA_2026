using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core.Managements.RoomDatas.ActionDatas;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using UnityEngine;

namespace _Project.Scripts.Rooms
{
    public class SelectCategoryForAction : PhaseCompletionSource<int>
    {
        public ActionData CurrentAction { get; private set; }
        
        public Category[] Categories { get; private set; }
        
        public SelectCategoryForAction(ActionData action)
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
            if (CurrentResult > 0)
            {
                var selectedSlot = new SelectNpcForCategory(Categories[CurrentResult]);
                selectedSlot.RunAndForget();
            }
            
            Categories = null;
            return base.Dispose(token);
        }
    }
}