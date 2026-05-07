using DefaultNamespace;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
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
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectActionForRoom phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            roomActionUIList.Disconnect();
            group.Hide();
            
            base.OnPhaseEnd(phase);
        }

        public void Cancel()
        {
            if (current != null)
                current.SetResult(-1);
        }

        public void ChooseAction(RoomAction actionData)
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
            if (!currentActionPoint.AddOrRemove(actionCost))
                return;
            Debug.Log($"Action {actionData.Name} cost {actionData.Cost} AP {currentActionPoint.Value} return {currentActionPoint.AddOrRemove(actionCost)}");
            var selectNpcsForAction = new SelectNpcsForAction(current.Choices[index]);
            selectNpcsForAction.RunAndForget();
            current.SetResult(index);
        }
    }
}