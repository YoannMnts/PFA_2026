using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay.Scripts
{
    public class Decision : IPhase<bool>
    {
        private bool endPhase;
        
        public Decision()
        {
            
        }
        
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            while (!endPhase)
                await Awaitable.NextFrameAsync(token);
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
        
        public void EndPhase(bool result)
        {
            endPhase = result;
        }
    }
}