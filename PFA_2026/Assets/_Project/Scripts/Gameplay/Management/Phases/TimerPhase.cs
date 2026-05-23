using System;
using System.Threading;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class TimerPhase : PhaseCompletionSource<bool>
    { 
        public event Action OnTimerRepeat;
        
        public int Duration { get; private set; }
        
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        private ManagementPhase currentPhase;
        public TimerPhase(ManagementPhase current ,int duration)
        {
            Duration = duration;
            currentPhase = current; 
        }

        protected override Awaitable Initialize(CancellationToken token)
        {
            Minutes = 0;
            Seconds = 0;
            Duration = 0;
            StartCountdown();
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            Duration = 0;
            Minutes = 0;
            Seconds = 0;
            return base.Dispose(token);
        }

        private async void StartCountdown()
        {
            try
            {
                int remaining;
                for (remaining = Duration ; 0 < remaining; remaining--)
                {
                    Minutes = remaining / 60;
                    Seconds = remaining % 60;
                    OnTimerRepeat?.Invoke();
                    await Awaitable.WaitForSecondsAsync(1);
                }
                SetResult(true);
                currentPhase.SetResult(true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}