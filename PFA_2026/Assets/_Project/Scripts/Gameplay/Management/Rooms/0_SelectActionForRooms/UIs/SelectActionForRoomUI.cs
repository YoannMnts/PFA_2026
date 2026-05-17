using System;
using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class SelectActionForRoomUI : MonoPhaseListener<SelectActionForRoom>
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private TMP_Text roomName;
        [SerializeField] private TMP_Text roomDescription;
        [SerializeField] private RoomActionUIList roomActionUIList;
        [SerializeField] private Button cancelButton;

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
            cancelButton.onClick.AddListener(Cancel);

            if (this.TryGetService(out PlayerController controller))
            {
                controller.PlayerInteractions.CanInteract.AddPriority(this, PriorityTags.High, false);
                controller.PlayerCamera.CanMove.AddPriority(this, PriorityTags.High, false);
            }
            
            phase.CurrentCineCamera.SwitchToThisCamera();
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
                controller.PlayerCamera.PlayerCam.SwitchToThisCamera();
            }

            base.OnPhaseEnd(phase);
        }

        private void Cancel()
        {
            Debug.Log($"Cancelling {current}");
            if (current == null) 
                return;
            
            current.Cancel();
        }

        public async void ChooseAction(RoomAction actionData)
        {
            try
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
            
                roomActionUIList.Disconnect();
                cancelButton.onClick.RemoveAllListeners();
                var fillCategory = new FillCategory(current.Choices[index], current.NpcSlots);
                var result = await fillCategory.Run();
            
                if (!result)
                {
                    var actionGain = current.Choices[index].Cost;
                    currentActionPoint.TryAddOrRemove(actionGain);
                    roomActionUIList.Connect(current.Choices);
                    cancelButton.onClick.AddListener(Cancel);
                    return;
                }
            
                current.CurrentRoom.SetActions(current.Choices[index]);
                current.SetResult(true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}