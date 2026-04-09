using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using UnityEngine;

namespace _Project.Scripts
{
    public class RoomUI : MonoBehaviour, IPhaseListener<SelectActionForRoom>
    {
        private void OnEnable()
        {
            this.Register();
        }

        private void OnDisable()
        {
            this.Unregister();
        }

        public void OnPhaseBegin(SelectActionForRoom phase)
        {
            
        }

        public void OnPhaseEnd(SelectActionForRoom phase)
        {
            
        }
    }
}