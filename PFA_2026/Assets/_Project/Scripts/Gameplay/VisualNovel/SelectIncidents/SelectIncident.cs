using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Naussilus.Gameplay
{
    public class SelectIncident : PhaseCompletionSource<Incident>
    {
        public Incident[] CurrentIncidents { get; private set; }

        public SelectIncident(Incident[] incidents)
        {
            CurrentIncidents = incidents;
        }
    }
}