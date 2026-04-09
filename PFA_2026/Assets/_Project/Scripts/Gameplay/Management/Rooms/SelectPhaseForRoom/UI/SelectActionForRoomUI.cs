using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managements.RoomDatas.ActionDatas;
using UnityEngine;

namespace _Project.Scripts
{
    public class SelectActionForRoomUI : MonoPhaseListener<SelectActionForRoom>
    {
        private SelectActionForRoom current;

        [SerializeField] private CanvasGroup group;
        [SerializeField] private RoomActionUIList roomActionUIList;
        
        
        protected override void OnPhaseBegin(SelectActionForRoom phase)
        {
            if(current != null)
                return;
            
            current = phase;
            roomActionUIList.Connect(phase.Choices);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectActionForRoom phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            roomActionUIList.Disconnect();
            
            base.OnPhaseEnd(phase);
        }

        public void Cancel()
        {
            if (current != null)
                current.SetResult(-1);
        }

        public void ChooseAction(ActionData actionData)
        {
            if(current == null)
                return;
            
            for (int i = 0; i < current.Choices.Length; i++)
            {
                if(current.Choices[i] == actionData)
                    current.SetResult(i);
            }
        }
    }
}