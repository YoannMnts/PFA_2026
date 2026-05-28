using System;
using System.Threading;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SwitchDay : PhaseCompletionSource<bool>
    {
        private readonly int second;
        public int CurrentDay { get; private set; }
        
        public int MaxDay { get; private set; }

        public SwitchDay(int timer, int day, int maxDay)
        {
            second = timer;
            CurrentDay = day;
            MaxDay = maxDay;
        }

        protected override Awaitable Initialize(CancellationToken token)
        {
            Delay();
            return base.Initialize(token);
        }

        private async void Delay()
        {
            try
            {
                await Awaitable.WaitForSecondsAsync(second);
                SetResult(true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}