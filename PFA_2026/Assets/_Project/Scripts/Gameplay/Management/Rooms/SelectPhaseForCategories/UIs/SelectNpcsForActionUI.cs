using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Rooms
{
    public class SelectNpcsForActionUI : MonoPhaseListener<SelectNpcsForAction>
    {
        private SelectNpcsForAction current;

        [SerializeField] private CanvasGroup group;
        [SerializeField] private CategoryUIList categoryUIList;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(SelectNpcsForAction phase)
        {
            if(current != null)
                return;
            
            current = phase;
            group.Show();
            categoryUIList.Connect(phase.CurrentAction.Categories);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectNpcsForAction phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            categoryUIList.Disconnect();
            group.Hide();
            
            base.OnPhaseEnd(phase);
        }

        public void Cancel()
        {
            if (current != null)
                current.SetResult(false);
        }

        public void ChooseCategory(Category category)
        {
            if(current == null)
                return;

            var index = 0;
            for (int i = 0; i < current.Categories.Length; i++)
            {
                if(current.Categories[i].Name == category.Name)
                    index = i;
            }

            var selectNpc = new SelectNpcForCategory(current.Categories[index]);
            
            selectNpc.RunAndForget();
        }

        public void Apply()
        {
            if (current != null)
                current.SetResult(false);
        }
    }
}