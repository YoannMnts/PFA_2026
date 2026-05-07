using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Rooms
{
    public class SelectNpcForCategoryUI : MonoPhaseListener<SelectNpcForCategory>
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private SlotNpcUIList slotNpcUIList;
        
        private SelectNpcForCategory current;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(SelectNpcForCategory phase)
        {
            if(current != null)
                return;
            
            current = phase;
            group.Show();
            slotNpcUIList.Connect(phase.Npcs);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectNpcForCategory phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            slotNpcUIList.Disconnect();
            group.Hide();
            
            base.OnPhaseEnd(phase);
        }

        public void Cancel()
        {
            if (current != null)
                current.SetResult(null);
        }

        public void ChooseNpc(Npc npc)
        {
            if (current == null)
                return;
            current.SetResult(npc);
        }
    }
}