using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers;
using UnityEngine;

namespace _Project.Scripts.Rooms
{
    public class SelectCategoryForActionUI : MonoPhaseListener<SelectCategoryForAction>
    {
        private SelectCategoryForAction current;

        [SerializeField] private CanvasGroup group;
        [SerializeField] private ActionCategoryUIList actionCategoryUIList;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(SelectCategoryForAction phase)
        {
            if(current != null)
                return;
            
            current = phase;
            group.Show();
            actionCategoryUIList.Connect(phase.CurrentAction.Categories);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectCategoryForAction phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            actionCategoryUIList.Disconnect();
            group.Hide();
            
            base.OnPhaseEnd(phase);
        }

        public void Cancel()
        {
            if (current != null)
                current.SetResult(-1);
        }

        public void ChooseCategory(Category category)
        {
            if(current == null)
                return;
            
            for (int i = 0; i < current.Categories.Length; i++)
            {
                if(current.Categories[i].Name == category.Name)
                    current.SetResult(i);
            }
        }
    }
}