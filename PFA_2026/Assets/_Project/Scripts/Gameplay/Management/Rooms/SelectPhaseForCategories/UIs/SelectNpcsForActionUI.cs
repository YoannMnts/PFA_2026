using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using UnityEngine;

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
            categoryUIList.Connect(phase.Categories);
            
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

        public void ChooseCategory(Category category, int slotIndex)
        {
            if(current == null)
                return;

            var index = 0;
            for (int i = 0; i < current.Categories.Length; i++)
            {
                if(current.Categories[i].Name == category.Name)
                    index = i;
            }

            var selectNpc = new SelectNpcForCategory(current.Categories[index], slotIndex);
            selectNpc.RunAndForget();
        }

        public void Apply()
        {
            for (int i = 0; i < current.Categories.Length; i++)
            {
                var category = current.Categories[i];
                for (int j = 0; j < category.CurrentNpcs.Length; j++)
                {
                    var npc = category.CurrentNpcs[j];
                    if (npc == null)
                    {
                        Debug.LogError($"Trying to apply without assign all npcs in category {category.Name}");
                        return;
                    }
                }
            }
            if (current != null)
                current.SetResult(false);
        }
    }
}