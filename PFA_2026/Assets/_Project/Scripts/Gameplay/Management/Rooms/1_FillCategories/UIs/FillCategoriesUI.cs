using System;
using System.Linq;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Gameplay.Buttons;
using Naussilus.Gameplay.CategoriesTitles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class FillCategoriesUI : MonoPhaseListener<FillCategory>, INpcClickListener
    {
        private FillCategory current;

        [SerializeField] private CanvasGroup group;
        [SerializeField] private CategoryUIList categoryUIList;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button applyButton;
        [SerializeField] private TMP_Text actionNameText;
        [SerializeField] private TMP_Text actionDescriptionText;

        public int NpcClickPriority { get; private set; } = 5;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(FillCategory phase)
        {
            if (current != null)
                return;

            current = phase;
            group.Show();
            categoryUIList.Connect(phase.Categories);
            closeButton.onClick.AddListener(Cancel);
            applyButton.onClick.AddListener(Apply);
            this.AddNpcClickListener();
            actionNameText.text = phase.CurrentAction.Name;
            actionDescriptionText.text = phase.CurrentAction.Description;

            for (int i = 0; i < current?.Categories.Length; i++)
            {
                var category = current.Categories[i];
                if (current.CurrentAction.ClearDefaultSlot)
                {
                    for (int j = 0; j < category.CategoryNpcSlots.Length; j++)
                    {
                        var categoryNpcSlot = category.CategoryNpcSlots[j];
                        categoryNpcSlot.TryRemoveDefaultNpc();
                    }
                }
                for (int j = 0; j < category.ObligateNpcs.Length; j++)
                {
                    var obligateNpc = category.ObligateNpcs[j];
                    AddNpcInCategory(obligateNpc);
                }
            }
            RefreshSelection();
            
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
            this.RemoveNpcClickListener();
            actionNameText.text = string.Empty;

            base.OnPhaseEnd(phase);
        }

        public void OnNpcClick(Npc npc)
        {
            AddNpcInCategory(npc);
        }


        public async void Cancel()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(closeButton.gameObject);
            }
            await Awaitable.WaitForSecondsAsync(0.2f);
            if (current == null)
                return;
            
            for (var i = 0; i < current.Categories.Length; i++)
            {
                var category = current.Categories[i];
                category.ClearAllSlots();
            }

            current.SetResult(false);
        }

        private void AddNpcInCategory(Npc npc)
        {
            if (current == null)
                return;

            for (int i = 0; i < current.Categories.Length; i++)
            {
                var category = current.Categories[i];
                if (!category.TryAddNpc(npc))
                    continue;
                RefreshSelection();
                return;
            }
        }

        public void RemoveNpcInCategory(Npc npc)
        {
            if (current == null)
                return;

            if (npc == null)
                return;

            for (int i = 0; i < current.Categories.Length; i++)
            {
                var category = current.Categories[i];
                category.TryRemoveNpc(npc);
                RefreshSelection();
            }
        }

        private async void Apply()
        {
            try
            {
                if (ButtonFeedbacksManager.instance != null)
                {
                    ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(applyButton.gameObject);
                }
                await Awaitable.WaitForSecondsAsync(0.2f);
                for (int i = 0; i < current.Categories.Length; i++)
                {
                    var category = current.Categories[i];
                    for (int j = 0; j < category.CategoryNpcSlots.Length; j++)
                    {
                        var roomNpcSlot = category.CategoryNpcSlots[j];
                        if (roomNpcSlot.CurrentNpc is null)
                        {
                            Debug.LogError($"Trying to apply without assign all npcs in category {category.Name}");
                            return;
                        }
                    }
                }
                
                current.CurrentAction.ComputeValidEffect();
                var consequenceSummary = new ActionConsequenceSummary(current.CurrentAction);
                var result = await consequenceSummary.Run();

                if (!result)
                    return;
                

                current?.SetResult(true);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void RefreshSelection()
        {
            foreach (var categoryUI in categoryUIList.UIItems)
            {
                foreach (var slot in categoryUI.UIList.UIItems)
                {
                    if (!slot.TrySelectSlot())
                        return;
                }
            }
        }
    }
}

