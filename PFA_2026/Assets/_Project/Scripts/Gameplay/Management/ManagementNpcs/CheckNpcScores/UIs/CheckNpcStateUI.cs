using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CheckNpcStateUI : MonoPhaseListener<CheckNpcState>
{
    private CheckNpcState current;

    [SerializeField] private CanvasGroup group;
    [SerializeField] private TMP_Text titleName;
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

        titleName.text = phase.CurrentNpc.Name;
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