using System;
using System.Collections.Generic;
using DG.Tweening;
using Naussilus.Core.Sounds;
using UnityEngine;

namespace Naussilus.Gameplay.Buttons
{
    public class ButtonFeedbacksManager : MonoBehaviour
    {
        public static ButtonFeedbacksManager instance;
        [SerializeField] private float baseButtonForce;
        [SerializeField] private float baseButtonDuration;
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
            if (currentButtons.Contains(button)==false)
            {
                if (SoundDesignManager.instance != null)
                {
                    SoundDesignManager.instance.PlaySound(SoundsEnum.Clic1,0.3f);
                }

                if (button.gameObject.activeInHierarchy)
                {
                   currentButtons.Add(button);
                    button.transform.DOScale(button.transform.localScale * (baseButtonForce * force), baseButtonDuration).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => Remove(button)); 
                }
            }
        }
        
        private void Remove(GameObject button)
        {
            currentButtons.Remove(button);
        }
    }
}
