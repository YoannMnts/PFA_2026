using System;
using System.Collections.Generic;
using Helteix.Singletons.SceneServices;
using Naussilus.Gameplay.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

namespace Naussilus.Gameplay
{
    public class PlayerInputs : PlayerComponent
    {
        public event Action<ITouchInput> OnTouch; 
        
        public Touchscreen CurrentTouchscreen => Touchscreens[0];
        public List<Touchscreen> Touchscreens { get; private set; }
        
        [ShowInInspector, ReadOnly]
        private List<ITouchInput> touchInputs;
        
        private void Awake()
        {
            Touchscreens = new List<Touchscreen>();
            touchInputs = new List<ITouchInput>();
        }

        private void Start()
        {
            for (int i = 0; i < InputSystem.devices.Count; i++)
            {
                if (InputSystem.devices[i] is Touchscreen touchscreen)
                {
                    AddTouchscreen(touchscreen);
                }
            }
        }

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        private void Update()
        {
            touchInputs.Sort();
            bool inputDidUpdate = false;
            foreach (var input in touchInputs)
            {
                if (inputDidUpdate)
                {
                    input.Sleep(this);
                }
                else if (input.Update(this))
                {
                    OnTouch?.Invoke(input);
                    inputDidUpdate = true;
                }
                else
                {
                    input.Sleep(this);
                }
            }
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (device is Touchscreen touchscreen)
            {
                if (change == InputDeviceChange.Added)
                {
                    AddTouchscreen(touchscreen);
                }

                if (change == InputDeviceChange.Removed)
                {
                    RemoveTouchscreen(touchscreen);
                }
                //Debug.Log($"OnDeviceChange: {change} -> {touchscreen.displayName}");
            }
        }

        private void AddTouchscreen(Touchscreen touchscreen)
        {
            if (Touchscreens.Contains(touchscreen)) return;
            Touchscreens.Add(touchscreen);
        }

        private void RemoveTouchscreen(Touchscreen touchscreen)
        {
            Touchscreens.Remove(touchscreen);
        }

        public void AddTouchInput(ITouchInput touchInput)
        {
            if (touchInputs.Contains(touchInput))
                return;
            
            touchInputs.Add(touchInput);
            foreach (var touchscreen in Touchscreens)
            {
                touchInput.AddTouchscreen(touchscreen, this);
            }
            
            touchInput.Enable(this);
        }

        public bool RemoveTouchInput(ITouchInput touchInput)
        {
            if (touchInputs.Remove(touchInput))
            {
                foreach (var touchscreen in Touchscreens)
                {
                    touchInput.RemoveTouchscreen(touchscreen, this);
                }
                touchInput.Disable(this);
                return true;
            }
            return false;
        }

        public static bool IsScreenPosOnUI(Vector2 screenPos)
        {
            EventSystem current = EventSystem.current;
            using(ListPool<RaycastResult>.Get(out var results))
            {
                var pointerEventData = new PointerEventData(EventSystem.current)
                {
                    position = screenPos,
                };
                current.RaycastAll(pointerEventData, results);
                if(results.Count > 0)
                    return false;
            }

            return true;
        }
    }
}