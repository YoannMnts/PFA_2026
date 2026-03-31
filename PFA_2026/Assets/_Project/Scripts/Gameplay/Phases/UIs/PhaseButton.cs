using System;
using Helteix.Tools.Phases;
using Naussilus.Core.Scripts.Managers;
using UnityEngine;

namespace Naussilus.Gameplay.Scripts.UIs
{
    public abstract class PhaseButton<T> : MonoBehaviour, IPhaseListener<T> where T : PhaseCompletionSource<bool>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        private T currentPhase;

        private void Start()
        {
            canvasGroup.Hide();
        }

        private void OnEnable()
        {
            this.Register();
        }

        private void OnDisable()
        {
            this.Unregister();
        }

        public virtual void OnPhaseBegin(T phase)
        {
            currentPhase = phase;
            canvasGroup.Show();
            StartDebugMethod();
        }

        public virtual void OnPhaseEnd(T phase)
        {
            currentPhase = null;
            canvasGroup.Hide();
            EndDebugMethod();
        }
        
        public virtual void OnButtonClick()
        {
            currentPhase?.SetResult(true);
        }

        private void StartDebugMethod()
        {
            Debug.Log($"Starting {currentPhase} phase");
        }

        private void EndDebugMethod()
        {
            Debug.Log($"Ending {currentPhase} phase");
        }
    }
}