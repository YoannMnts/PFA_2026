using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using UnityEngine;

namespace _Project.Scripts
{
    public class NpcCheckMenu : IActionPhase
    {
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            var waitInMenu = new WaitInMenu();
            await waitInMenu.Run();
            return waitInMenu.CurrentResult;
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