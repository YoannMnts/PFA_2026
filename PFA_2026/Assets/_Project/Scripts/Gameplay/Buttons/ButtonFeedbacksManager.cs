using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Naussilus.Gameplay.Buttons
{
    public class ButtonFeedbacksManager : MonoBehaviour
    {
        public static ButtonFeedbacksManager instance;
        [SerializeField] private float baseButtonForce;
        private List<GameObject> currentButtons;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            currentButtons = new List<GameObject>();
        }

        public void ApplyButtonFeedbacks(GameObject button, float force = 1f)
        {
            Debug.Log("ApplyButtonFeedbacks: " + button);
            if (currentButtons.Contains(button)==false)
            {
                currentButtons.Add(button);
                button.transform.DOScale(baseButtonForce*button.transform.localScale*force, 0.3f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => Remove(button));
            }
        }
        
        private void Remove(GameObject button)
        {
            currentButtons.Remove(button);
        }
    }
}
