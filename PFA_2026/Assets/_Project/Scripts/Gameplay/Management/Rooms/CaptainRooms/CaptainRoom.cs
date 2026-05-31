using Helteix.Tools.Phases;

namespace Naussilus.Gameplay
{
    public class CaptainRoom : PhaseCompletionSource<bool>
    {
        public void EndDay() => currentPhase?.SetResult(true);
        public void SwitchCamera() => cineCamera?.SwitchToThisCamera();
        
        private readonly ManagementPhase currentPhase;
        private MonoCineCamera cineCamera;
        public CaptainRoom(ManagementPhase phase, MonoCineCamera roomCineCamera)
        {
            currentPhase = phase; 
            cineCamera = roomCineCamera;
        }
    }
}