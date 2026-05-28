using System;
using Helteix.Tools.Phases.Listeners;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SwitchDayBoatUI : MonoPhaseListener<SwitchDay>
    {
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float boatTravelTime = 1f;
        [SerializeField] private float UIDistance = 500f;
        [SerializeField] private float curveMultiplier = 1f;
        private float currentProgress = 0f;
        private Vector2 basePosition;
        private RectTransform boat;

        private void Awake()
        {
            boat = GetComponent<RectTransform>();
            basePosition = boat.anchoredPosition;
        }

        protected override async void OnPhaseBegin(SwitchDay phase)
        {
            base.OnPhaseBegin(phase);
            float goalProgress = (float)phase.CurrentDay / phase.MaxDay;
            Debug.Log(goalProgress);
            float distance = goalProgress - currentProgress;
            Debug.Log(distance);
            float speed = distance / boatTravelTime;
            Debug.Log(speed);
            while (currentProgress < goalProgress)
            {
                currentProgress += speed*Time.deltaTime;
                Vector2 newPosition = new Vector2(basePosition.x + (currentProgress*UIDistance), basePosition.y + curve.Evaluate(currentProgress)*curveMultiplier);
                boat.anchoredPosition = newPosition;
                await Awaitable.NextFrameAsync();
            }
        }
    }
}