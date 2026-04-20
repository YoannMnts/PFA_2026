using System.Threading;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.VisualNovels.EventDatas;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class VisualNovelPhase : IPhase<bool>
    {
        private readonly EventData currentEvent;
        public NpcData NpcEventData => currentEvent.Npcs[0];

        public VisualNovelPhase(EventData eventData)
        {
            currentEvent = eventData;
        }
        
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            DialogueData dialogueData = currentEvent.FirstDialogue;
            
            Dialogue dialoguePhase = new Dialogue(NpcEventData ,dialogueData);
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