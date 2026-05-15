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

        void ITouchInput.AddTouchscreen(Touchscreen touchscreen, PlayerInputs playerInputs)
        {
            
        }

        bool ITouchInput.RemoveTouchscreen(Touchscreen touchscreen, PlayerInputs playerInputs)
        {
            return true;
        }

        bool ITouchInput.Update(PlayerInputs playerInputs)
        {

            foreach (var touchscreen in playerInputs.Touchscreens)
            {
                if(Process(touchscreen))
                    return true;

            }
            return false;
        }

        void ITouchInput.Sleep(PlayerInputs playerInputs)
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


            var touch0Pos = touch0.position.ReadValue();
            var touch1Pos = touch1.position.ReadValue();
            
            if(PlayerInputs.IsScreenPosOnUI(touch0Pos) || PlayerInputs.IsScreenPosOnUI(touch1Pos))
                return false;
            
            float currentDistance = Vector2.Distance(
                touch0Pos,
                touch1Pos
            );

            PreviousDistance = Vector2.Distance(
                touch0Pos - touch0.delta.ReadValue(),
                touch1Pos - touch1.delta.ReadValue()
            );

            Delta = PreviousDistance - currentDistance;
            return Mathf.Abs(Delta) > 10;
        }
    }
}