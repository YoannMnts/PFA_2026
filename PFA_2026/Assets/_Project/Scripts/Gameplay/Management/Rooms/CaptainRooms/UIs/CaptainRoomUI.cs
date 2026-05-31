using System;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class CaptainRoomUI : MonoPhaseListener<CaptainRoom>
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private Button captainRoomButton;

        private CaptainRoom current;
        
        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(CaptainRoom phase)
        {
            if (current != null)
                current.Cancel();
            
            current = phase;
            group.Show();
            captainRoomButton.onClick.AddListener(OnButtonClicked);
            current.SwitchCamera();
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(CaptainRoom phase)
        {
            if (current == null)
                return;
            
            group.Hide();
            captainRoomButton.onClick.RemoveListener(OnButtonClicked);
            if (gameObject.TryGetService(out PlayerController controller))
            {
                controller.PlayerCamera.PlayerCam.SwitchToThisCamera();
            }
            current = null;
            base.OnPhaseEnd(phase);
        }

        private void OnButtonClicked()
        {
            current.EndDay();
        }
    }
}