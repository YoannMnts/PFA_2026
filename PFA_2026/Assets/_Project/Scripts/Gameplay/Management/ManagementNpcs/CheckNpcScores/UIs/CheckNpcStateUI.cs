using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;

public class CheckNpcStateUI : MonoPhaseListener<CheckNpcState>
{
    private CheckNpcState current;

    [SerializeField] private CanvasGroup group;
    [SerializeField] private BehaviorUIList behaviorUIList;
    [SerializeField] private MentalStateUIList mentalStateUIList;

    private void Start()
    {
        group.Hide();
    }

    protected override void OnPhaseBegin(CheckNpcState phase)
    {
        if(current != null)
            return;
            
        current = phase;
        group.Show();
        behaviorUIList.Connect(phase.NpcBehaviors);
        mentalStateUIList.Connect(phase.NpcMentalStates);
            
        base.OnPhaseBegin(phase);
    }

    protected override void OnPhaseEnd(CheckNpcState phase)
    {
        if (current != phase) 
            return;
            
        current = null;
        behaviorUIList.Disconnect();
        mentalStateUIList.Disconnect();
        group.Hide();
            
        base.OnPhaseEnd(phase);
    }

    public void Cancel()
    {
        if (current != null)
            current.SetResult(true);
    }
}