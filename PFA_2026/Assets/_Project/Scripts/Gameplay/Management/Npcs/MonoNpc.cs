using DG.Tweening;
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
        
        private CategoryNpcSlot currentSlot;
        private CategoryNpcSlot lastSlot;
        private bool burstAnimationIsPlaying = false;

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            npcSprite.transform.DOScaleY(0.315f, 0.9f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
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
            lastSlot = currentSlot;
            currentSlot = slot;
            gameObject.transform.position = slot.SlotPosition;
            Debug.Log($"Npc {Npc.Name} is setting new position to {slot.SlotPosition}");
            slot.NpcExpression.TryGetExpression(Npc, out var sprite);
            npcSprite.sprite = sprite;
            npcSprite.flipX = slot.Flip;
            npcSprite.sortingOrder = slot.OrderInLayer;
            npcSprite.sortingLayerName = slot.SpriteSortingLayerName;
            if (!burstAnimationIsPlaying)
            {
                burstAnimationIsPlaying = true;
                npcSprite.transform.DOScale(0.36f, 0.15f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => burstAnimationIsPlaying = false);
            }
        }

        private void RemoveSlot()
        {
            if (lastSlot == null)
                return;
            //Debug.Log($"Npc {Npc.Name} is returning to the last position {lastPosition}");
            gameObject.transform.position = lastSlot.SlotPosition;
            lastSlot.NpcExpression.TryGetExpression(Npc, out var sprite);
            npcSprite.sprite = sprite;
            npcSprite.flipX = lastSlot.Flip;
            npcSprite.sortingOrder = lastSlot.OrderInLayer;
            npcSprite.sortingLayerName = lastSlot.SpriteSortingLayerName;
        }
    }
}