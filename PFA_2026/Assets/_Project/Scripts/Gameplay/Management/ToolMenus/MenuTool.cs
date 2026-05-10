using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;

public class MenuTool : MonoPhaseListener<ManagementPhase>
{
    private ActionPoint currentActionPoint;
    
    protected override void OnPhaseBegin(ManagementPhase phase)
    {
        currentActionPoint = phase.CurrentActionPoint;
        base.OnPhaseBegin(phase);
    }

    public void OnShipClicked()
    {
        var selectRoomForShip = new SelectRoomForShip(currentActionPoint);
        selectRoomForShip.RunAndForget();
    }

    public void OnLogClicked()
    {
            
    }

    public void OnOptionsClicked()
    {
            
    }
}