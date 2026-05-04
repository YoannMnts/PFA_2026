using System;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Rooms
{
    public class ResumePhaseForActionUI : MonoPhaseListener<ResumePhaseForAction>
    {
        private ResumePhaseForAction current;

        [field: SerializeField] private CanvasGroup group;

        [field: SerializeField] private ConsequenceTextUIList consequenceTextUIList;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(ResumePhaseForAction phase)
        {
            if(current != null)
                return;
            
            current = phase;
            group.Show();
            //consequenceTextUIList.Connect();
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ResumePhaseForAction phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            group.Hide();
            
            base.OnPhaseEnd(phase);
        }

        public void Apply()
        {
            current.CurrentRoomAction.AddAllValidEffect();
            current.SetResult(true);
            Debug.Log($"Apply");
        }

        public void Abort()
        {
            current.SetResult(false);
            Debug.Log($"Abort");
        }
    }
}