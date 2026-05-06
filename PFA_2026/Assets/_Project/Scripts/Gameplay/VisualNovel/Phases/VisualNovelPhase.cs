using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel
{
    public class VisualNovelPhase : IPhase<bool>
    {
        private readonly Incident currentEvent;
        private Dialogue dialogue;
        public Npc NpcEventData => currentEvent.Npcs[0];

        public VisualNovelPhase(Incident eventData)
        {
            currentEvent = eventData;
        }
        
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            dialogue = currentEvent.FirstDialogue;

            while (true)
            {
                ReadDialogue readDialogue = new ReadDialogue(dialogue.Lines);
                await readDialogue.Run();

                SelectAnswer selectAnswer = new SelectAnswer(dialogue.Answers);
                await selectAnswer.Run();

                if (selectAnswer.CurrentResult is not BasicAnswer basicAnswer)
                    break;
                
                dialogue = basicAnswer.NextDialogue;
            }
            
            SelectAnswer selectDecision = new SelectAnswer(dialogue.Answers);
            await selectDecision.Run();

            if (selectDecision.CurrentResult is FinalAnswer finalAnswer)
            {
                ReadDialogue readDialogue = new ReadDialogue(finalAnswer.NpcText);
                await readDialogue.Run();

                for (int i = 0; i < finalAnswer.Effects.Length; i++)
                    finalAnswer.Effects[i].ComputeConditionalEffect(NpcEventData);
                
                var summary = new Summary();
                await summary.Run();
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