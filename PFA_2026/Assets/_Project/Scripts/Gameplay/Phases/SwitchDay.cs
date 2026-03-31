using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay.Scripts
{
    public class SwitchDay : IPhase<bool>
    {
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            await Awaitable.WaitForSecondsAsync(1f);
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