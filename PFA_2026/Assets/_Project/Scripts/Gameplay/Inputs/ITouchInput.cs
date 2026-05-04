using System;
using UnityEngine.InputSystem;

namespace Naussilus.Gameplay
{
    public interface ITouchInput : IComparable<ITouchInput>
    {
        int Priority { get; }
        
        void AddTouchscreen(Touchscreen touchscreen, PlayerInputManager playerInputManager);
        bool RemoveTouchscreen(Touchscreen touchscreen, PlayerInputManager playerInputManager);

        bool Update(PlayerInputManager playerInputManager);
        void Sleep(PlayerInputManager playerInputManager);

        int IComparable<ITouchInput>.CompareTo(ITouchInput other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }
}