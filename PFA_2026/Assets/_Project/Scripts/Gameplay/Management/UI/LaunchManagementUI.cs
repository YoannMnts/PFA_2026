using Helteix.Tools.Phases;
using Naussilus.Core.Scripts.Managers;
using UnityEngine;

namespace _Project.Scripts
{
    public class LaunchManagementUI : MonoBehaviour, IPhaseListener<ManagementPhase>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
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

        public void OnPhaseBegin(ManagementPhase phase)
        {
            canvasGroup.Show();
        }

        public void OnPhaseEnd(ManagementPhase phase)
        {
            canvasGroup.Hide();
        }
    }
}