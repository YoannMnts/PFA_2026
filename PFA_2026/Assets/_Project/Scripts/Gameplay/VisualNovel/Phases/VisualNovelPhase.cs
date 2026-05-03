using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel
{
    public class VisualNovelPhase : IPhase<bool>
    {
        private readonly Incident currentEvent;
        public Npc NpcEventData => currentEvent.Npcs[0];

        public VisualNovelPhase(Incident eventData)
        {
            currentEvent = eventData;
        }
        
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            Dialogue dialogueData = currentEvent.FirstDialogue;
            
            DialoguePhase dialoguePhase = new DialoguePhase(NpcEventData ,dialogueData);
            await dialoguePhase.Run();
            
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