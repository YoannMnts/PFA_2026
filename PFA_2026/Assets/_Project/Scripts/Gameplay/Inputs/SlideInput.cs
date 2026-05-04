using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Naussilus.Gameplay
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
        
        void ITouchInput.AddTouchscreen(Touchscreen touchscreen, PlayerInputManager playerInputManager)
        {
            
        }

        bool ITouchInput.RemoveTouchscreen(Touchscreen touchscreen, PlayerInputManager playerInputManager)
        {
            return true;
        }

        bool ITouchInput.Update(PlayerInputManager playerInputManager)
        {
            if (!inputAction.IsInProgress()) 
                return false;
            if (inputAction.activeControl.device is not Touchscreen touchscreen || !playerInputManager.Touchscreens.Contains(touchscreen))
                return false;
                
            Delta = inputAction.ReadValue<Vector2>();
            return true;
        }

        void ITouchInput.Sleep(PlayerInputManager playerInputManager)
        { 
            Delta = Vector2.zero;
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