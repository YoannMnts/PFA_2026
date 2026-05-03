using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;

namespace Rooms
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
            {
                for (var i = 0; i < current.Categories.Length; i++)
                {
                    var category = current.Categories[i];
                    category.ClearNpc();
                }

                current.SetResult(false);
            }
        }

        public void ChooseCategory(Category category, int ind)
        {
            if(current == null)
                return;

            var index = 0;
            for (int i = 0; i < current.Categories.Length; i++)
            {
                if(current.Categories[i] == category)
                    index = i;
            }
            var selectNpc = new SelectNpcForCategory(current.Categories[index], ind);
            selectNpc.RunAndForget();
        }

        public void Apply()
        {
            for (int i = 0; i < current.Categories.Length; i++)
            {
                var category = current.Categories[i];
                for (int j = 0; j < category.CurrentNpcs.Count; j++)
                {
                    var npc = category.CurrentNpcs[j];
                    if (npc is null)
                    {
                        Debug.LogError($"Trying to apply without assign all npcs in category {category.Name}");
                        return;
                    }
                }
            }
            current.CurrentAction.AddAllValidEffect();

            current?.SetResult(true);
        }
    }
}