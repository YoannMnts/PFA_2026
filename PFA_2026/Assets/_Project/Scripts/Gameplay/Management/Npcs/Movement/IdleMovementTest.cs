using System;
using UnityEngine;
using DG.Tweening;

namespace Naussilus.Gameplay
{
    public class IdleMovementTest : MonoBehaviour
    {
        [SerializeField] private GameObject NPCSprite;
        [SerializeField] private GameObject UIImage;
        private bool isIdling = true;
        [SerializeField] private float idleSpeed = 1.5f;
        [SerializeField] private float idleScale = 1.05f;
        [SerializeField] private float burstSpeed = 1.5f;
        [SerializeField] private float burstScale = 1.02f;
        [SerializeField] private float buttonStr = 2.1f;
        [SerializeField] private float buttonDuration = 0.6f;
        

        private void Awake()
        {
            NPCSprite.transform.DOScaleY(idleScale, idleSpeed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }

        public void Burst()
        {
            NPCSprite.transform.position = new Vector3(0, 0, 0);
            NPCSprite.transform.DOScale(burstScale, burstSpeed).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
        }

        public void TPOut()
        {
            NPCSprite.transform.position = new Vector3(100, 100, 100);
        }

        public void Stop()
        {
            NPCSprite.transform.DOKill();
            NPCSprite.transform.localScale = Vector3.one;
        }

        public void ButtonClicked()
        {
            UIImage.transform.DOScale(buttonStr, buttonDuration).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
        }
    }
}
