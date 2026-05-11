using System;
using UnityEngine.InputSystem;

namespace Naussilus.Gameplay
{
    public interface ITouchInput : IComparable<ITouchInput>
    {
        int Priority { get; }
        
        void AddTouchscreen(Touchscreen touchscreen, PlayerInputs playerInputs);
        bool RemoveTouchscreen(Touchscreen touchscreen, PlayerInputs playerInputs);

        bool Update(PlayerInputs playerInputs);
        void Sleep(PlayerInputs playerInputs);
        void Disable(PlayerInputs playerInputs){}
        void Enable(PlayerInputs playerInputs){}
        int IComparable<ITouchInput>.CompareTo(ITouchInput other)
        {
            return other.Priority.CompareTo(Priority);
        }
    }
}