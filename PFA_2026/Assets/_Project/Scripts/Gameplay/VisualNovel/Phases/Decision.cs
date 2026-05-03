using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel
{
    public class Decision : IPhase<bool>
    {
        private readonly Npc currentNpcData;
        
        public IAnswer[] CurrentAnswers { get; private set; }
        public Decision(Npc npcData ,IAnswer[] answers)
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
                case BasicAnswer basicAnswerData:
                {
                    var nextDialogue = basicAnswerData.NextDialogue;
                    DialoguePhase dialogue = new DialoguePhase(currentNpcData ,nextDialogue);
                    await dialogue.Run();
                    break;
                }
                
                case FinalAnswer finalAnswerData:
                    Npc data = currentNpcData;
                    ConditionalEffect[] effects = finalAnswerData.Effects;
                    foreach (var effect in effects)
                        effect.ComputeConditionalEffect(data);
                    var summary = new Summary();
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