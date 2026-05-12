using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Gameplay.Player.Interactions;
using UnityEngine;

namespace Naussilus.Gameplay.Management.ManagementNpcs
{
    public class MonoNpc : MonoPhaseListener<ManagementPhase>, IInteractable
    {
        public int Priority { get; private set; }

        [SerializeField] private NpcData npcData;
    
        private Vector2 lastPosition;

        public Npc Npc => NpcManager.TryGetNpc(npcData?.GUID);
    
        private CheckNpcState checkNpcPhase;
        
        public void Interact(PlayerInteractions playerInteractions)
        {
            checkNpcPhase = new CheckNpcState(this);
            checkNpcPhase.RunAndForget();
        }
        
        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            checkNpcPhase?.SetResult(false);
            checkNpcPhase = null;
            base.OnPhaseEnd(phase);
        }

        public bool IsInteractable()
        {
            return false;
        }
    }
}