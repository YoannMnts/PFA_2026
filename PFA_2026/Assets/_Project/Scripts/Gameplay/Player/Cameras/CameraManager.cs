using System.Collections.Generic;
using Unity.Cinemachine;

namespace Naussilus.Gameplay
{
    public static class CameraManager
    {
        private static List<CinemachineCamera> cameras = new List<CinemachineCamera>();
        
        public static CinemachineCamera ActiveCamera { get; private set; }

        public static void Register(this CinemachineCamera newCamera)
        {
            cameras.Add(newCamera);
        }

        public static void Unregister(this CinemachineCamera newCamera)
        {
            cameras.Remove(newCamera);
        }

        public static bool IsActiveCamera(this CinemachineCamera newCamera)
        {
            return ActiveCamera == newCamera;
        }

        public static void SwitchToThisCamera(this CinemachineCamera newCamera)
        {
            if (newCamera.IsActiveCamera())
                return;
            
            newCamera.Priority = 10;
            ActiveCamera = newCamera;

            for (int i = 0; i < cameras.Count; i++)
            {
                if (!cameras[i].IsActiveCamera())
                    cameras[i].Priority = 0;
                
            }
        }
    }
}