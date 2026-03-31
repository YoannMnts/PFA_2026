using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay.Scripts
{
    public class SwitchDay : IPhase<bool>
    {
        private readonly int second;

        public SwitchDay(int timer)
        {
            second = timer;
        }
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            await Awaitable.WaitForSecondsAsync(second);
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