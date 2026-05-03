using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SwitchDay : IPhase<bool>
    {
        private readonly int second;
        public int CurrentDay { get; private set; }

        public SwitchDay(int timer, int day)
        {
            second = timer;
            CurrentDay = day;
        }


        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            await Awaitable.WaitForSecondsAsync(second, token);
            return true;
        }

        async Awaitable IPhase<bool>.Initialize(CancellationToken token)
        {
            await Task.CompletedTask;
        }

        async Awaitable IPhase<bool>.Dispose(CancellationToken token)
        {
            await Task.CompletedTask;
        }
    }
}