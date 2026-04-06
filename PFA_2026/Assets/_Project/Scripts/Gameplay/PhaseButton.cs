using Helteix.Tools.Phases;
using Naussilus.Core.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    [RequireComponent(typeof(Button))]
    public abstract class PhaseButton<T> : MonoBehaviour, IPhaseListener<T> where T : IBasePhase
    {
        [SerializeField]
        protected CanvasGroup canvasGroup;
        
        protected Button button;
        protected T currentPhase;

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

        protected abstract void OnButtonClicked();

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