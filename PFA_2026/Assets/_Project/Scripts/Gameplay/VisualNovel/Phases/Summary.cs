using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel
{
    public class Summary : IPhase<bool>
    {
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            var summaryWaitPhase = new SummaryWait();
            await summaryWaitPhase.Run();
            return summaryWaitPhase.CurrentResult;
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