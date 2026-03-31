using System;
using Helteix.Tools.Phases;
using Naussilus.Core.Scripts.Managers;
using UnityEngine;

namespace Naussilus.Gameplay.Scripts.UIs
{
    public class SwitchDayUI : MonoBehaviour, IPhaseListener<SwitchDay>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        private SwitchDay currentSwitchDay;

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

        public void OnPhaseBegin(SwitchDay phase)
        {
            currentSwitchDay = phase;
            canvasGroup.Show();
        }

        public void OnPhaseEnd(SwitchDay phase)
        {
            canvasGroup.Hide();
            currentSwitchDay = null;
        }
    }
}