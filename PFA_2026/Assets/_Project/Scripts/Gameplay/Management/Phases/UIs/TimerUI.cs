using System;
using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class TimerUI : MonoPhaseListener<ManagementPhase>
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private int timerDuration;
        [SerializeField] private bool isTimerActive;
        
        private ManagementPhase currentPhase;
        private TimerPhase timer;

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            currentPhase = phase;
            if (isTimerActive)
            {
                timer?.Cancel();
                
                timer = new TimerPhase(timerDuration);
                
                timer.OnTimerRepeat += OnTimerRepeat;
                timer.OnTimerEnd += OnTimerEnd;
                
                timer.RunAndForget();
            }
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            currentPhase = null;
            timerText.text = string.Empty;
            timerDuration = 0;
            timer.OnTimerRepeat -= OnTimerRepeat;
            timer.OnTimerEnd -= OnTimerEnd;
            timer.Cancel();
            base.OnPhaseEnd(phase);
        }

        private void OnTimerRepeat()
        {
            timerText.text = $"{timer.Minutes:01}:{timer.Seconds:01}";
        }

        private void OnTimerEnd()
        {
            throw new NotImplementedException();
        }
    }
}