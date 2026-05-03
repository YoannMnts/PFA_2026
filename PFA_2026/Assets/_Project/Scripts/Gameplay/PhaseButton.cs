using Helteix.Tools.Phases;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    [RequireComponent(typeof(Button))]
    public abstract class PhaseButton<T> : MonoBehaviour, IPhaseListener<T> where T : IBasePhase
    {
        protected Button button;
        protected T currentPhase;

        private void OnEnable()
        {
            button = GetComponent<Button>();
            this.Register();
        }

        private void OnDisable()
        {
            this.Unregister();
        }

        public virtual void OnPhaseBegin(T phase)
        {
            currentPhase = phase;
            button.onClick.AddListener(OnButtonClicked);
        }

        public virtual void OnPhaseEnd(T phase)
        {
            button.onClick.RemoveAllListeners();
            currentPhase = default;
        }

        public void DisableInteraction()
        {
            button.interactable = false;
        }

        protected abstract void OnButtonClicked();
        
    }
}