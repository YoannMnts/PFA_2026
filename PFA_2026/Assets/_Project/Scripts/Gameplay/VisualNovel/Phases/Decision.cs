using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core.Datas.VisualNovels;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class Decision : IPhase<bool>
    {
        public Answer[] CurrentAnswers { get; private set; }
        public Decision(Answer[] answers)
        {
            CurrentAnswers = answers;
        }
        
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            var decisionChoice = new DecisionChoice();
            await decisionChoice.Run();
            var answerIndex = decisionChoice.CurrentResult;

            switch (CurrentAnswers[answerIndex])
            {
                case BasicAnswerData basicAnswerData:
                {
                    var nextDialogue = basicAnswerData.NextDialogue;
                    Dialogue dialogue = new Dialogue(nextDialogue);
                    await dialogue.Run();
                    break;
                }
                case FinalAnswerData finalAnswerData:
                    Summary summary = new Summary(finalAnswerData);
                    await summary.Run();
                    break;
            }
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