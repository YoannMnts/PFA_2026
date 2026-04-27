using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Core.NpcDatas;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Rooms
{
    public class SelectNpcForCategoryUI : MonoPhaseListener<SelectNpcForCategory>
    {
        [SerializeField] private CanvasGroup group;
        [FormerlySerializedAs("categorySlotUIList")] [SerializeField] private CategoryNpcUIList categoryNpcUIList;
        
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
            categoryNpcUIList.Connect(phase.Npcs);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectNpcForCategory phase)
        {
            if (current != phase) 
                return;
            
            current = null;
            categoryNpcUIList.Disconnect();
            group.Hide();
            
            base.OnPhaseEnd(phase);
        }

        public void ChooseNpc(NpcData npcData)
        {
            if (current == null)
                return;
            
            current.SetResult(npcData);
        }
    }
}