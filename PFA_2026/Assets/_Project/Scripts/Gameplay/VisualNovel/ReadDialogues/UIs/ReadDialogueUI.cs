using System;
using Helteix.Tools.Phases.Listeners;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class ReadDialogueUI : MonoPhaseListener<ReadDialogue>, IPointerClickHandler
    {
        [SerializeField] 
        private TMP_Text dialogueText;
        
        [SerializeField] 
        private TMP_Text characterName;
        
        [SerializeField]
        private Image npcImage;

        private bool isDialogueRead;

        protected override async void OnPhaseBegin(ReadDialogue phase)
        {
            try
            {
                for (int i = 0; i < phase.DialogueLines.Length; i++)
                {
                    var dialogueLine = phase.DialogueLines[i];
                    characterName.text = dialogueLine.Npc.Name;
                    dialogueLine.Expression.TryGetExpression(dialogueLine.Npc, out Sprite sprite);
                    npcImage.sprite = sprite;
                    for (int j = 0; j < dialogueLine.Text.Length; j++)
                    {
                        dialogueText.text = dialogueLine.Text[j];
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

        public void OnPointerClick(PointerEventData eventData)
        {
            isDialogueRead = true;
        }
    }
}