using System;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using Naussilus.Gameplay.Management.Phases;
using Naussilus.Gameplay.Player.Interactions;
using UnityEngine;

namespace Naussilus.Gameplay.Management.ManagementNpcs
{
    public class MonoNpc : MonoPhaseListener<ManagementPhase>, IInteractable, INpcClickListener
    {
        public int Priority { get; private set; } = 5;
        public int NpcClickPriority { get; private set; } = 1;

        [SerializeField] private NpcData npcData;
        
        public Npc Npc => NpcManager.TryGetNpc(npcData?.GUID);
        
        private ManagementPhase currentPhase;
        private CheckNpcState checkNpcPhase;

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            currentPhase = phase;
            ManagementPhase.AddNpcClickListener(this);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            ManagementPhase.RemoveNpcClickListener(this);
            currentPhase = null;
            
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

        public bool IsInteractable()
        {
            return true;
        }
    }
}