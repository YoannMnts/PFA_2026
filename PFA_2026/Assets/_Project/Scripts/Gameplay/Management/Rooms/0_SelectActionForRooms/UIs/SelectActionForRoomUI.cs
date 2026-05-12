using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Rooms;
using Naussilus.Gameplay.Player;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class SelectActionForRoomUI : MonoPhaseListener<SelectActionForRoom>
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private TMP_Text roomName;
        [SerializeField] private TMP_Text roomDescription;
        [SerializeField] private RoomActionUIList roomActionUIList;

        public string Name => current.CurrentRoom.Name;
        public string Description => current.CurrentRoom.Description;

        private SelectActionForRoom current;
        private ActionPoint currentActionPoint;
        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(SelectActionForRoom phase)
        {
            if(current != null)
                current.Cancel();
            
            current = phase;
            group.Show();
            roomName.text = Name;
            roomDescription.text = Description;
            currentActionPoint = phase.CurrentActionPoint;
            roomActionUIList.Connect(phase.Choices);

            if (this.TryGetService(out PlayerController controller))
            {
                controller.PlayerInteractions.CanInteract.AddPriority(this, PriorityTags.High, false);
                controller.PlayerCamera.CanMove.AddPriority(this, PriorityTags.High, false);
            }
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectActionForRoom phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            roomActionUIList.Disconnect();
            group.Hide();

            if (this.TryGetService(out PlayerController controller))
            {
                controller.PlayerInteractions.CanInteract.RemovePriority(this);
                controller.PlayerCamera.CanMove.RemovePriority(this);
            }

            base.OnPhaseEnd(phase);
        }

        public void Cancel()
        {
            if (current != null)
                current.SetResult(false);
        }

        public async void ChooseAction(RoomAction actionData)
        {
            if(current == null)
                return;

            var index = 0;
            for (int i = 0; i < current.Choices.Length; i++)
            {
                if(current.Choices[i] == actionData)
                    index = i;
            }
            
            var actionCost = -current.Choices[index].Cost;
            if (!currentActionPoint.TryAddOrRemove(actionCost))
                return;
            
            Debug.Log($"Action {actionData.Name} cost {actionData.Cost} AP {currentActionPoint.Value} return {currentActionPoint.TryAddOrRemove(actionCost)}");
            var fillCategory = new FillCategory(current.Choices[index]);
            var result = await fillCategory.Run();
            if (!result)
            {
                current.CurrentRoom.TryGetCurrentAction(out var action);
                currentActionPoint.TryAddOrRemove(action.Cost);
                return;
            }
            
            current.SetResult(true);
        }
    }
}