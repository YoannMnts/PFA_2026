using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay.Scripts.UIs
{
    public abstract class PhaseButton : MonoBehaviour, IPhaseListener<PhaseCompletionSource<bool>>
    {
        private PhaseCompletionSource<bool> currentPhase;
        
        public virtual void OnPhaseBegin(PhaseCompletionSource<bool> phase)
        {
            this.Register();
            currentPhase = phase;
        }

        public virtual void OnPhaseEnd(PhaseCompletionSource<bool> phase)
        {
            this.Unregister();
            currentPhase = null;
        }
        
        public virtual void OnButtonClick()
        {
            currentPhase?.SetResult(true);
        }
    }
}