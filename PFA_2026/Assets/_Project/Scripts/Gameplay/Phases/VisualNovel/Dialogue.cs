using System.Threading;
using Helteix.Tools.Phases;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Naussilus.Gameplay.Scripts
{
    public class Dialogue : IPhase<bool>
    {
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            await Task.CompletedTask;
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
