using System;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class TimerUI : MonoPhaseListener<TimerPhase>
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private bool isTimerActive;
        [SerializeField] private CanvasGroup group;

        private TimerPhase currentTimer;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(TimerPhase phase)
        {
            if (currentTimer != null)
                currentTimer.Cancel();
            
            
            currentTimer = phase;
            currentTimer.OnTimerRepeat += OnTimerRepeat;
            group.Show();
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(TimerPhase phase)
        {
            if (currentTimer == null)
                return;
            
            timerText.text = string.Empty;
            group.Hide();
            currentTimer.OnTimerRepeat -= OnTimerRepeat;
            currentTimer = null;
            
            base.OnPhaseEnd(phase);
        }

        private void OnTimerRepeat()
        {
            timerText.text = $"{currentTimer.Minutes:01}:{currentTimer.Seconds:01}";
        }

        private void OnTimerEnd()
        {
            throw new NotImplementedException();
        }
    }
}