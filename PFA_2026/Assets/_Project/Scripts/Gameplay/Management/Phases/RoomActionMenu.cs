using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using UnityEngine;

namespace _Project.Scripts
{
    public class RoomActionMenu : IPhase<bool>
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