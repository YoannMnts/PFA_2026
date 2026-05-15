using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Naussilus.Gameplay
{
    [Serializable]
    public class TapInput : ITouchInput
    {
        private readonly InputAction inputAction;
        public Vector2 TapPosition { get; private set; }
        int ITouchInput.Priority => 5;
        
        public TapInput() : this(new InputAction("TapInput",  InputActionType.Button, "<Touchscreen>/press", "tap"))
        {
            
        }
        
        public TapInput(InputAction inputAction)
        {
            this.inputAction = inputAction;
        }
        void ITouchInput.AddTouchscreen(Touchscreen touchscreen, PlayerInputs playerInputs)
        {
            
        }

        bool ITouchInput.RemoveTouchscreen(Touchscreen touchscreen, PlayerInputs playerInputs)
        {
            return true;
        }

        bool ITouchInput.Update(PlayerInputs playerInputs)
        {
            if (!inputAction.WasPerformedThisDynamicUpdate()) 
                return false;
            
            TapPosition = playerInputs.CurrentTouchscreen.primaryTouch.position.ReadValue();
            
            
            return !PlayerInputs.IsScreenPosOnUI(TapPosition);
        }

        void ITouchInput.Sleep(PlayerInputs playerInputs)
        {
            
        }
        
        void ITouchInput.Enable(PlayerInputs playerInputs)
        {
            inputAction.Enable();
        }

        void ITouchInput.Disable(PlayerInputs playerInputs)
        {
            inputAction.Disable();
        }
    }
}