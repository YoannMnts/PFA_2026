using System;
using Helteix.Tools.Phases.Listeners;
using UnityEngine;

namespace Naussilus.Gameplay.Parrallax
{
    public class BoatOscillation : MonoPhaseListener<ManagementPhase>
    {
        [SerializeField] private GameObject ship;
        [SerializeField] private Camera camera;
        [SerializeField] private AnimationCurve boatOscillationCurve;
        [SerializeField] private float oscillationSpeed;
        [SerializeField] private float oscillationInstensity;
        private float currentCurveProgress;
        private bool isOscillating = false;
        private Vector3 basePosition;

        private void Awake()
        {
            basePosition = ship.transform.position;
        }

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            base.OnPhaseBegin(phase);
            isOscillating = true;
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            base.OnPhaseEnd(phase);
            isOscillating = false;
        }

        private void Update()
        {
            Debug.Log(isOscillating);
            Debug.Log(camera.orthographicSize);
            if (isOscillating && camera.orthographicSize > 3f)
            {
                Oscillate();
            }
        }

        private void Oscillate()
        {
            currentCurveProgress += oscillationSpeed*Time.deltaTime;
            if (currentCurveProgress > 1)
            {
                currentCurveProgress = 0;
            }
            ship.transform.position = basePosition + Vector3.up * (boatOscillationCurve.Evaluate(currentCurveProgress) * oscillationInstensity);
        }
    }
}
