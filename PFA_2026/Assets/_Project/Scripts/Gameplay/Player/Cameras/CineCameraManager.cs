using System.Collections.Generic;
using Unity.Cinemachine;

namespace Naussilus.Gameplay
{
    public static class CineCameraManager
    {
        private static List<CinemachineCamera> cameras = new List<CinemachineCamera>();
        
        public static CinemachineCamera ActiveCamera { get; private set; }

        public static void Register(this ICineCameraListener newCamera)
        {
            cameras.Add(newCamera.CineCamera);
        }

        public static void Unregister(this ICineCameraListener newCamera)
        {
            cameras.Remove(newCamera.CineCamera);
        }

        public static bool IsActiveCamera(this CinemachineCamera newCamera)
        {
            return ActiveCamera == newCamera;
        }

        public static void SwitchToThisCamera(this ICineCameraListener listener)
        {
            CinemachineCamera cineCamera = listener.CineCamera;
            if (!cameras.Contains(cineCamera))
                return;
                
            if (cineCamera.IsActiveCamera())
                return;
            
            cineCamera.Priority = 10;
            ActiveCamera = cineCamera;

            for (int i = 0; i < cameras.Count; i++)
            {
                if (!cameras[i].IsActiveCamera())
                    cameras[i].Priority = 0;
                
            }
        }
    }
}