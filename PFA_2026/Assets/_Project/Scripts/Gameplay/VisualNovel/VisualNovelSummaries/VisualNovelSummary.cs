using System.Collections.Generic;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers;

namespace Naussilus.Gameplay
{
    public class VisualNovelSummary : PhaseCompletionSource<bool>
    {
        public List<Consequence> CurrentConsequences => ConsequenceManager.ValidConsequences;
    }
}