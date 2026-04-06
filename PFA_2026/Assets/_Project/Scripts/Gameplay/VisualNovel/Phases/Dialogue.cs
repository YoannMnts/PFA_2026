using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core.Datas.VisualNovels;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class Dialogue : IPhase<bool>
    {
        public DialogueData CurrentDialogue { get; private set; }

        public Dialogue(DialogueData dialogue)
        {
            CurrentDialogue = dialogue;
        }
        
        async Awaitable<bool> IPhase<bool>.Execute(CancellationToken token)
        {
            await Awaitable.WaitForSecondsAsync(3f, token);
            
            Answer[] answers = CurrentDialogue.Answers;
            Decision decision = new Decision(answers);
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
