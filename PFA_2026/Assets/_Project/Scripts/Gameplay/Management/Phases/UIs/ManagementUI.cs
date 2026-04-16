using System;
using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using UnityEngine;

namespace _Project.Scripts
{
    public class ManagementUI : MonoBehaviour, IPhaseListener<ManagementPhase>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        private void Awake()
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