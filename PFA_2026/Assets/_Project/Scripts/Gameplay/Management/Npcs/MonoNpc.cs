using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Gameplay.Interactions;
using Unity.Cinemachine;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class MonoNpc : MonoPhaseListener<ManagementPhase>, IInteractable, INpcClickListener
    {
        public int Priority { get; private set; } = 5;
        public int NpcClickPriority { get; private set; } = 1;

        [SerializeField] private NpcData npcData;
        
        [SerializeField] private MonoCineCamera npcCamera;
        
        [SerializeField] private SpriteRenderer npcSprite;
        
        public Npc Npc => NpcManager.TryGetNpc(npcData?.GUID);
        public MonoCineCamera NpcCamera => npcCamera;
        
        private ManagementPhase currentPhase;
        private CheckNpcState checkNpcPhase;
        
        private Vector3 lastPosition;
        

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            currentPhase = phase;
            this.AddNpcClickListener();
            Npc.OnAddedInSlot += AddedInSlot;
            Npc.OnRemoveSlot += RemoveSlot;

            if (Npc.CurrentCategory == null)
            {
                Npc.AddToRandomSlot();
            }
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            this.RemoveNpcClickListener();
            currentPhase = null;
            Npc.OnAddedInSlot -= AddedInSlot;
            Npc.OnRemoveSlot -= RemoveSlot;
            
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

        private void AddedInSlot(CategoryNpcSlot slot)
        {
            lastPosition = gameObject.transform.position;
            gameObject.transform.position = slot.SlotPosition;
            Debug.Log($"Npc {Npc.Name} is setting new position to {slot.SlotPosition}");
            slot.NpcExpression.TryGetExpression(Npc, out var sprite);
            npcSprite.sprite = sprite;
            npcSprite.flipX = slot.Flip;
            npcSprite.sortingOrder = slot.OrderInLayer;
            npcSprite.sortingLayerName = slot.SpriteSortingLayerName;
        }

        private void RemoveSlot()
        {
            //Debug.Log($"Npc {Npc.Name} is returning to the last position {lastPosition}");
            gameObject.transform.position = lastPosition;
            npcSprite.sprite = null;
            npcSprite.flipX = false;
            npcSprite.sortingOrder = 0;
            npcSprite.sortingLayerName = "Default";
        }
    }
}