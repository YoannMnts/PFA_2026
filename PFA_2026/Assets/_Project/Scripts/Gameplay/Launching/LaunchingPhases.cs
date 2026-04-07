using System;
using System.Threading.Tasks;
using _Project.Scripts;
using Helteix.Tools.Phases;
using Naussilus.Core.Datas.VisualNovels;
using Naussilus.Gameplay.VisualNovel._Project.Scripts;
using UnityEngine;

namespace Naussilus.Gameplay.Launcher._Project.Scripts.Gameplay
{
    public class LaunchingPhases : MonoBehaviour
    {
        [field: SerializeField] 
        private int maxDay;
        
        [field: SerializeField] 
        private int switchDayWaitSeconds;
        

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

        private static async Awaitable VisualNovel()
        {
            EventData visualNovelEvent = EventManager.GetValidEvent();
            var visualNovelPhase = new VisualNovelPhase(visualNovelEvent);
            await visualNovelPhase.Run();
        }

        private static async Awaitable Management()
        {
            var sideView = new ManagementPhase();
            await sideView.Run();
        }
    }
}