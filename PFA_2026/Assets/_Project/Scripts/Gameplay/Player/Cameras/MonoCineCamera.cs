using Unity.Cinemachine;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public abstract class MonoCineCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cineCamera;

        protected virtual void OnEnable()
        {
            cineCamera.Register();
        }

        protected virtual void OnDisable()
        {
            cineCamera.Unregister();
        }

        public void SwitchToThisCamera()
        {
            cineCamera.SwitchToThisCamera();
        }
    }
}