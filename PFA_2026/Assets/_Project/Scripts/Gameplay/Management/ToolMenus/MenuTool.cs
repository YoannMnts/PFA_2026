using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;

public class MenuTool : MonoPhaseListener<ManagementPhase>
{
    private ActionPoint currentActionPoint;
    
    private ManagementPhase currentPhase;

    protected override void OnPhaseBegin(ManagementPhase phase)
    {
        currentPhase = phase;
        currentActionPoint = phase.CurrentActionPoint;
        base.OnPhaseBegin(phase);
    }

    protected override void OnPhaseEnd(ManagementPhase phase)
    {
        currentPhase = null;
        currentActionPoint = null;
        
        base.OnPhaseEnd(phase);
    }

    public void OnShipClicked()
    {
        var selectRoomForShip = new SelectRoomForShip(currentActionPoint, currentPhase);
        selectRoomForShip.RunAndForget();
    }

    public void OnLogClicked()
    {
            
    }

    public void OnOptionsClicked()
    {
            
    }
}