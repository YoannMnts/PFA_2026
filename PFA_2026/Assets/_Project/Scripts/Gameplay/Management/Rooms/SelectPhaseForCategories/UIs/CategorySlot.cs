using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Rooms
{
    [RequireComponent(typeof(Button))]
    public class CategorySlot : MonoBehaviour
    {
        public Button Button { get; private set; }
        
        private void OnEnable()
        {
            Button = GetComponent<Button>();
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveAllListeners();
        }
    }
}