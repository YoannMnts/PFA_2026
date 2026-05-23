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

        public SwitchDay(int timer, int day)
        {
            second = timer;
            CurrentDay = day;
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