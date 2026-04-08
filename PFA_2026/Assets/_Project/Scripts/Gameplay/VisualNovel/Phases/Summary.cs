using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class Summary : IPhase<bool>
    {
        public FinalAnswerData CurrentFinalAnswerData { get; private set; }
        
        public Summary(FinalAnswerData finalAnswerData)
        {
            CurrentFinalAnswerData = finalAnswerData;
        }
        
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