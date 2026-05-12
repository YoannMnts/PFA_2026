using System;
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
        public int Priority { get; private set; } = 5;

        [SerializeField] private NpcData npcData;
        
        public Npc Npc => NpcManager.TryGetNpc(npcData?.GUID);
        
        private ManagementPhase currentPhase;
        private CheckNpcState checkNpcPhase;

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            currentPhase = phase;
            phase.OnNpcClicked += TryCheckNpc;
            Debug.Log($"Management phase begin for npc {Npc.Name}");
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            currentPhase.OnNpcClicked -= TryCheckNpc;
            currentPhase = null;
            
            base.OnPhaseEnd(phase);
        }

        private void TryCheckNpc(Npc npc)
        {
            Debug.Log($"[MonoNpc] Checking npc {npc.Name}");
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
        }

        public bool IsInteractable()
        {
            return true;
        }
    }
}