using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class TimerUI : MonoPhaseListener<TimerPhase>
    {
        [SerializeField] private Image fillImage;
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
            
            if (isTimerActive)
                currentTimer.StartCountdown();
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(TimerPhase phase)
        {
            if (currentTimer == null)
                return;
            
            fillImage.fillAmount = 1;
            group.Hide();
            currentTimer.OnTimerRepeat -= OnTimerRepeat;
            currentTimer = null;
            
            base.OnPhaseEnd(phase);
        }

        private void OnTimerRepeat()
        {
            //Debug.Log($"Remaining time : {currentTimer.Remaining}, Duration : {currentTimer.Duration}");
            fillImage.fillAmount = (float)currentTimer.Remaining / currentTimer.Duration;
        }
    }
}