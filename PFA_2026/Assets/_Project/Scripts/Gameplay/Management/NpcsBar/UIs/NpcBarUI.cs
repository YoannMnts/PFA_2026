using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;

public class NpcBarUI : MonoPhaseListener<ManagementPhase>
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private NpcSlotUIList npcSlotUIList;
        
    private ManagementPhase currentPhase;

    private void Start()
    {
        group.Hide();
    }

    protected override void OnPhaseBegin(ManagementPhase phase)
    {
        if (currentPhase != null)
            return;
            
        currentPhase = phase;
        group.Show();
        npcSlotUIList.Connect(phase.CurrentNpcs);
            
        base.OnPhaseBegin(phase);
    }

    protected override void OnPhaseEnd(ManagementPhase phase)
    {
        if (currentPhase == null)
            return;
            
        currentPhase = null;
        group.Hide();
        npcSlotUIList.Disconnect();
            
        base.OnPhaseEnd(phase);
    }
}