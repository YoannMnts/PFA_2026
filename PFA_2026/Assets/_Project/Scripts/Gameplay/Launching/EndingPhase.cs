using System.Threading;
using Helteix.Tools.Phases;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naussilus.Gameplay
{
    public class EndingPhase : PhaseCompletionSource<bool>
    {
        public bool IsGameOver { get; private set; }
        
        public EndingPhase(bool isGameOver)
        {
            IsGameOver = isGameOver;
        }

        protected override async Awaitable Initialize(CancellationToken token)
        {
            await SceneManager.LoadSceneAsync(2);
            base.Initialize(token);
        }
    }
}