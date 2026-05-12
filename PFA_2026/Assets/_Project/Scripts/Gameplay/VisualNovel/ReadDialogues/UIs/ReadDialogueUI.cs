using System;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Gameplay.Player.Interactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.VisualNovel
{
    public class ReadDialogueUI : MonoPhaseListener<ReadDialogue>, IInteractable
    {
        [SerializeField] 
        private TMP_Text text;
        
        [SerializeField]
        private Image bgImage;

        private bool isDialogueRead;
        public int Priority { get; private set; } = 10;

        protected override async void OnPhaseBegin(ReadDialogue phase)
        {
            try
            {
                for (int i = 0; i < phase.DialogueLines.Length; i++)
                {
                    for (int j = i + 1; j < phase.DialogueLines[i].Text.Length; j++)
                    {
                        text.text = phase.DialogueLines[i].Text[j];
                        isDialogueRead = false;
                        while (!isDialogueRead)
                            await Awaitable.NextFrameAsync();
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

        public bool IsInteractable()
        {
            return true;
        }

        public void Interact(PlayerInteractions playerInteractions)
        {
            isDialogueRead = true;
        }
    }
}