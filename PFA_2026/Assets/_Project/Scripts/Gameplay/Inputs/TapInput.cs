using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Naussilus.Gameplay
{
    [Serializable]
    public class TapInput : ITouchInput
    {
        private readonly InputAction inputAction;
        int ITouchInput.Priority => 5;

        public event Action<Vector2> OnTap;
        
        public TapInput() : this(new InputAction("TapInput",  InputActionType.Button, "<Touchscreen>/press", "tap"))
        {
            
        }
        
        public TapInput(InputAction inputAction)
        {
            this.inputAction = inputAction;
        }
        void ITouchInput.AddTouchscreen(Touchscreen touchscreen, PlayerInputManager playerInputManager)
        {
            
        }

        bool ITouchInput.RemoveTouchscreen(Touchscreen touchscreen, PlayerInputManager playerInputManager)
        {
            return true;
        }

        bool ITouchInput.Update(PlayerInputManager playerInputManager)
        {
            if (!inputAction.WasPerformedThisDynamicUpdate()) 
                return false;
            
            var pos = playerInputManager.CurrentTouchscreen.primaryTouch.position.ReadValue();
            OnTap?.Invoke(pos);
            return true;
        }

        void ITouchInput.Sleep(PlayerInputManager playerInputManager)
        {
            
        }
        
        void ITouchInput.Enable(PlayerInputManager playerInputManager)
        {
            inputAction.Enable();
        }

        void ITouchInput.Disable(PlayerInputManager playerInputManager)
        {
            inputAction.Disable();
        }
    }
}