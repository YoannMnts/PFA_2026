using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class VisualNovelPhase : IPhase<bool>
    {
        private readonly Incident[] currentEvents;
        
        private Incident currentEvent;
        private Dialogue dialogue;
        public Npc NpcEventData => currentEvent.Npcs[0];

        public VisualNovelPhase(Incident[] eventDatas)
        {
            currentEvents = eventDatas;
        }
        
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            var selectIncident = new SelectIncident(currentEvents);
            var incidentResult = await selectIncident.Run();
            
            currentEvent = incidentResult.value;
            dialogue = currentEvent.FirstDialogue;

            PhaseResult<IAnswer> result;
            while (true)
            {
                ReadDialogue readDialogue = new ReadDialogue(dialogue.Lines);
                await readDialogue.Run();
                Debug.Log($"finish ReadDialogue");

                SelectAnswer selectAnswer = new SelectAnswer(dialogue.Answers);
                result = await selectAnswer.Run();
                
                if (result.value is not BasicAnswer basicAnswer)
                    break;
                
                dialogue = basicAnswer.NextDialogue;
            }
            
            Debug.Log($"Decision choices : {dialogue.Answers.Length}");
            if (result.value is FinalAnswer finalAnswer)
            {
                ReadDialogue readDialogue = new ReadDialogue(finalAnswer.NpcText);
                await readDialogue.Run();

                for (int i = 0; i < finalAnswer.Effects.Length; i++)
                    finalAnswer.Effects[i].ComputeConditionalEffect(NpcEventData);
                currentEvent.AddToCompletedEvent();
            }
            
            var summary = new VisualNovelSummary();
            await summary.Run();
            
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