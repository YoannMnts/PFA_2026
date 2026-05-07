using System;
using Naussilus.Core;
using Naussilus.Core.Managers;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class CountdownRoomUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text countdownText;
        
        [SerializeField]
        private TMP_Text titleName;

        [SerializeField] private CanvasGroup group;

        private void Start()
        {
            Disconnect();
        }

        public void Connect(int countdown ,RoomAction action)
        {
            group.Show();
            countdownText.text = $"Jour restant : {countdown}";
            titleName.text = action.Name;
        }

        public void Disconnect()
        {
            group.Hide();
            countdownText.text = string.Empty;
            titleName.text = string.Empty;
        }
    }
}