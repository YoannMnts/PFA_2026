using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.VisualNovels.EventDatas;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class Decision : IPhase<bool>
    {
        private readonly NpcData currentNpcData;
        
        public Answer[] CurrentAnswers { get; private set; }
        public Decision(NpcData npcData ,Answer[] answers)
        {
            CurrentAnswers = answers;
            currentNpcData = npcData;
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
                    Dialogue dialogue = new Dialogue(currentNpcData ,nextDialogue);
                    await dialogue.Run();
                    break;
                }
                
                case FinalAnswerData finalAnswerData:
                    NpcData data = currentNpcData;
                    ConditionalEffect[] effects = finalAnswerData.Effects;
                    foreach (var effect in effects)
                        effect.ComputeConditionalEffect(data);
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