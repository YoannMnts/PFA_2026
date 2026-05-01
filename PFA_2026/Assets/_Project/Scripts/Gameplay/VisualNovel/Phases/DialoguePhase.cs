using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class DialoguePhase : IPhase<bool>
    {
        private readonly Npc currentNpcData;
        
        public Dialogue CurrentDialogue { get; private set; }
        
        public DialoguePhase(Npc npcData ,Dialogue dialogue)
        {
            CurrentDialogue = dialogue;
            currentNpcData = npcData;
        }
        
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            await Awaitable.WaitForSecondsAsync(3f, token);
            
            IAnswer[] answers = CurrentDialogue.Answers;
            Decision decision = new Decision(currentNpcData ,answers);
            await decision.Run();
            
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
