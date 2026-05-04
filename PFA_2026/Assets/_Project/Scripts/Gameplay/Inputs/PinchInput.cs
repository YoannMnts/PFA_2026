using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Naussilus.Gameplay
{ 
    [Serializable]
    public class PinchInput : ITouchInput
    {
        [field: ShowInInspector, ReadOnly]
        public float Delta { get; private set; }
        [field: ShowInInspector, ReadOnly]
        public float PreviousDistance { get; private set; }
        int ITouchInput.Priority => 10;

        void ITouchInput.AddTouchscreen(Touchscreen touchscreen, PlayerInputManager playerInputManager)
        {
            
        }

        bool ITouchInput.RemoveTouchscreen(Touchscreen touchscreen, PlayerInputManager playerInputManager)
        {
            return true;
        }

        bool ITouchInput.Update(PlayerInputManager playerInputManager)
        {

            foreach (var touchscreen in playerInputManager.Touchscreens)
            {
                if(Process(touchscreen))
                    return true;

            }
            return false;
        }

        void ITouchInput.Sleep(PlayerInputManager playerInputManager)
        {
            Delta = 0;
        }

        private bool Process(Touchscreen touchscreen)
        {
            if (touchscreen == null) 
                return false;

            var touchCount = touchscreen.touches.Count(ctx => ctx.isInProgress);
            if (touchCount != 2)
                return false;

            var touch0 = touchscreen.touches[0];
            var touch1 = touchscreen.touches[1];

            float currentDistance = Vector2.Distance(
                touch0.position.ReadValue(),
                touch1.position.ReadValue()
            );

            PreviousDistance = Vector2.Distance(
                touch0.position.ReadValue() - touch0.delta.ReadValue(),
                touch1.position.ReadValue() - touch1.delta.ReadValue()
            );

            Delta = PreviousDistance - currentDistance;
            return Mathf.Abs(Delta) > 10;
        }
    }
}