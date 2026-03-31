using System;
using Helteix.Tools.Phases;
using Naussilus.Core.Scripts.Managers;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.Scripts.UIs
{
    [RequireComponent(typeof(Button))]
    public abstract class PhaseButton<T> : MonoBehaviour, IPhaseListener<T> where T : IPhaseCompletionSource<bool>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        private Button button;
        private T currentPhase;

        private void Start()
        {
            button = GetComponent<Button>();
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
            button.onClick.AddListener(OnButtonClicked);
            StartDebugMethod();
        }

        public virtual void OnPhaseEnd(T phase)
        {
            button.onClick.RemoveAllListeners();
            canvasGroup.Hide();
            EndDebugMethod();
            currentPhase = default;
        }
        
        protected virtual void OnButtonClicked()
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