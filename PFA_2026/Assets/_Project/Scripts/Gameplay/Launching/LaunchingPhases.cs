using System;
using System.Threading.Tasks;
using _Project.Scripts;
using Helteix.Tools.Phases;
using Naussilus.Core.Datas.VisualNovels;
using Naussilus.Core.Scripts.Managers;
using Naussilus.Gameplay.VisualNovel._Project.Scripts;
using UnityEngine;

namespace Naussilus.Gameplay.Launcher._Project.Scripts.Gameplay
{
    public class LaunchingPhases : MonoBehaviour
    {
        [SerializeField] 
        private int maxDay;
        
        [SerializeField] 
        private int switchDayWaitSeconds;

        [SerializeField] 
        private CanvasGroup visualNovelCanvas;

        [SerializeField] 
        private CanvasGroup managementCanvas;

        private void Start()
        {
            PhaseLifetime();
        }

        private async void PhaseLifetime()
        {
            try
            {
                for (int i = 0; i < maxDay; i++)
                {
                    var switchDay = new SwitchDay(switchDayWaitSeconds, i);
                    await switchDay.Run();
                    await VisualNovel();
                    await Management();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private async Awaitable VisualNovel()
        {
            EventData visualNovelEvent = EventManager.GetValidEvent();
            
            var visualNovelPhase = new VisualNovelPhase(visualNovelEvent);
            await visualNovelPhase.Run();
        }

        private async Awaitable Management()
        {
            var sideView = new ManagementPhase();
            await sideView.Run();
        }
    }
}