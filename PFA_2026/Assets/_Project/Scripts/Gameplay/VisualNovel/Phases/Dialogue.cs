using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class Dialogue : IPhase<bool>
    {
        private readonly NpcData currentNpcData;
        
        public DialogueData CurrentDialogue { get; private set; }
        
        public Dialogue(NpcData npcData ,DialogueData dialogue)
        {
            CurrentDialogue = dialogue;
            currentNpcData = npcData;
        }
        
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            await Awaitable.WaitForSecondsAsync(3f, token);
            
            AnswerData[] answers = CurrentDialogue.Answers;
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
