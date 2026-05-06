using System;
using Helteix.Tools.Phases.Listeners;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.VisualNovel
{
    public class ReadDialogueUI : MonoPhaseListener<ReadDialogue>
    {
        [SerializeField] 
        private TMP_Text text;
        
        [SerializeField]
        private Image bgImage;
        
        protected override async void OnPhaseBegin(ReadDialogue phase)
        {
            try
            {
                for (int i = 0; i < phase.DialogueLines.Length; i++)
                {
                    for (int j = i + 1; j < phase.DialogueLines[i].Text.Length; j++)
                    {
                        text.text = phase.DialogueLines[i].Text[j];
                        await Awaitable.WaitForSecondsAsync(1);
                    }
                }
                base.OnPhaseBegin(phase);
                
                phase.SetResult(true);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}