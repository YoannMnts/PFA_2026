using System;
using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core.Sounds;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class TimerPhase : PhaseCompletionSource<bool>
    { 
        public event Action OnTimerRepeat;
    
        public int Duration { get; private set; }
    
        private ManagementPhase currentPhase;
        public int Remaining { get; private set; }
    
        private CancellationTokenSource cts;

        public TimerPhase(ManagementPhase current, int duration)
        {
            Duration = duration;
            currentPhase = current;
            cts = new CancellationTokenSource();
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            cts.Cancel();
            cts.Dispose();
            return base.Dispose(token);
        }

        public async void StartCountdown()
        {
            try
            {
                for (Remaining = Duration; 0 < Remaining; Remaining--)
                {
                    if (cts.Token.IsCancellationRequested) 
                        return;
                
                    OnTimerRepeat?.Invoke();
                    if (Remaining < 6)
                    {
                        if (SoundDesignManager.instance != null)
                        {
                            SoundDesignManager.instance.PlaySound(SoundsEnum.TimerEnd);
                        }
                    }
                    await Awaitable.WaitForSecondsAsync(1, cts.Token);
                }
                SetResult(true);
                currentPhase.SetResult(true);
            }
            catch (OperationCanceledException)
            {
                Debug.Log($"Timed has cancelled");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}