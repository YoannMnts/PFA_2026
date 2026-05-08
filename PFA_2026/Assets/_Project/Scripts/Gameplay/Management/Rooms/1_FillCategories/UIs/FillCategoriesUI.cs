using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms
{
    public class FillCategoriesUI : MonoPhaseListener<FillCategory>
    {
        private FillCategory current;

        [SerializeField] private CanvasGroup group;
        [SerializeField] private CategoryUIList categoryUIList;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button applyButton;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(FillCategory phase)
        {
            if(current != null)
                return;
            
            current = phase;
            group.Show();
            categoryUIList.Connect(phase.Categories);
            closeButton.onClick.AddListener(Cancel);
            applyButton.onClick.AddListener(Apply);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(FillCategory phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            categoryUIList.Disconnect();
            group.Hide();
            closeButton.onClick.RemoveAllListeners();
            applyButton.onClick.RemoveAllListeners();
            
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

        public async void ChooseCategory(Category category, int slotIndex)
        {
            if(current == null)
                return;

            var index = 0;
            for (int i = 0; i < current.Categories.Length; i++)
            {
                if (current.Categories[i] == category)
                {
                    index = i;
                    break;
                }
            }
            var selectNpc = new SelectNpcForCategory(current.Categories[index], slotIndex);
            var npcResult = await selectNpc.Run();
            current.Categories[index].AddNpc(npcResult.value, slotIndex);
        }

        public async void Apply()
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

            var consequenceSummary = new ActionConsequenceSummary(current.CurrentAction);
            var result  = await consequenceSummary.Run();

            if (!result)
                return;
            
            current?.SetResult(true);
        }
    }
}