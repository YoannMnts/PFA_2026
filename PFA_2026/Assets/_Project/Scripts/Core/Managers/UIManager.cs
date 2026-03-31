using UnityEngine;

namespace Naussilus.Core.Scripts.Managers
{
    public static class UIManager
    {
        public static void Show(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
            Debug.Log("AAAAAAAAA");
        }

        public static void Hide(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            Debug.Log("BBBBBBB");
        }
    }
}