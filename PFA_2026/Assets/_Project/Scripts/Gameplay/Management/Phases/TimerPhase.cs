using System;
using System.Threading;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class TimerPhase : PhaseCompletionSource<bool>
    {
        public event Action OnTimerEnd;
        public event Action OnTimerRepeat;
        
        public int Duration { get; private set; }
        
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public TimerPhase(int duration)
        {
            Duration = duration;
        }

        protected override Awaitable Initialize(CancellationToken token)
        {
            Minutes = 0;
            Seconds = 0;
            StartCountdown();
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            Duration = 0;
            Minutes = 0;
            Seconds = 0;
            OnTimerEnd?.Invoke();
            return base.Dispose(token);
        }

        private async void StartCountdown()
        {
            try
            {
                for (Duration = 0; 0 < Duration; Duration--)
                {
                    Minutes = Duration / 60;
                    Seconds = Duration % 60;
                    OnTimerRepeat?.Invoke();
                    await Awaitable.WaitForSecondsAsync(1);
                }
                SetResult(true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}