using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Gameplay.Interactions;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class MonoNpc : MonoPhaseListener<ManagementPhase>, IInteractable, INpcClickListener
    {
        public int Priority { get; private set; } = 5;
        public int NpcClickPriority { get; private set; } = 1;

        [SerializeField] private NpcData npcData;
        
        public Npc Npc => NpcManager.TryGetNpc(npcData?.GUID);
        
        private ManagementPhase currentPhase;
        private CheckNpcState checkNpcPhase;
        
        private Vector3 lastPosition;

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            currentPhase = phase;
            this.AddNpcClickListener();
            Npc.OnSetNewPosition += SetNewPosition;
            Npc.OnReturnToLastPosition += ReturnToLastPosition;
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            this.RemoveNpcClickListener();
            currentPhase = null;
            Npc.OnSetNewPosition -= SetNewPosition;
            Npc.OnReturnToLastPosition -= ReturnToLastPosition;
            
            base.OnPhaseEnd(phase);
        }

        public void OnNpcClick(Npc npc)
        {
            TryCheckNpc(npc);
        }
        

        private void TryCheckNpc(Npc npc)
        {
            if (npc != Npc)
                return;

            if (checkNpcPhase != null)
            {
                checkNpcPhase.Cancel();
                checkNpcPhase = null;
                return;
            }
            
            Interact(null);
        }

        public void Interact(PlayerInteractions playerInteractions)
        {
            if (currentPhase == null)
                return;
            
            checkNpcPhase = new CheckNpcState(this);
            checkNpcPhase.RunAndForget();
            Debug.Log($"Npc {Npc.Name} is interacting");
        }

        private void SetNewPosition(Transform newTransform)
        {
            Debug.Log($"Npc {Npc.Name} is setting new position to {newTransform.position}");
            lastPosition = gameObject.transform.position;
            gameObject.transform.position = newTransform.position;
        }

        private void ReturnToLastPosition()
        {
            Debug.Log($"Npc {Npc.Name} is returning to the last position {lastPosition}");
            gameObject.transform.position = lastPosition;
        }
    }
}