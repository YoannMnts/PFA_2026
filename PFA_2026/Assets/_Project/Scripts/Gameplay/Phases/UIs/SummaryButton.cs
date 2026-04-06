using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay.Scripts.UIs
{
    public class SummaryButton : PhaseButton<Summary>
    {
        protected override void OnButtonClicked()
        {
            currentPhase.SetResult(true);
        }
    }
}