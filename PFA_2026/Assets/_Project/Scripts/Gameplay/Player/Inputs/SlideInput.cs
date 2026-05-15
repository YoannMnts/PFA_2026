using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Naussilus.Gameplay.Player
{
    [Serializable]
    public class SlideInput : ITouchInput
    {
        
        [ShowInInspector, ReadOnly]
        public Vector2 Delta { get; private set; }
        
        int ITouchInput.Priority => 5;

        private readonly InputAction inputAction;

        public SlideInput() : this(new InputAction("SlideInput",  InputActionType.Value, "<Touchscreen>/delta"))
        {
            
        }
        public SlideInput(InputAction inputAction)
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
            if (!inputAction.IsInProgress()) 
                return false;
            if (inputAction.activeControl.device is not Touchscreen touchscreen || !playerInputs.Touchscreens.Contains(touchscreen))
                return false;
                
            Delta = inputAction.ReadValue<Vector2>();
            return true;
        }

        void ITouchInput.Sleep(PlayerInputs playerInputs)
        { 
            Delta = Vector2.zero;
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