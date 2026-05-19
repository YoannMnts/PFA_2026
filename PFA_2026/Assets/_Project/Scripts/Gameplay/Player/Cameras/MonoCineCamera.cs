using Unity.Cinemachine;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public abstract class MonoCineCamera : MonoBehaviour, ICineCameraListener
    {
        [field: SerializeField]
        public CinemachineCamera CineCamera { get; private set; }

        protected virtual void OnEnable()
        {
            this.Register();
        }

        protected virtual void OnDisable()
        {
            this.Unregister();
        }
    }
}