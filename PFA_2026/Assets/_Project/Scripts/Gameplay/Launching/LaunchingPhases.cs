using System;
using System.Threading.Tasks;
using _Project.Scripts;
using Helteix.Tools.Phases;
using Naussilus.Core.VisualNovels.EventDatas;
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
                    //await SwitchDay(i);
                    //await VisualNovel();
                    bool result = await Management();
                    if (!result)
                    {
                        GameOver();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void GameOver()
        {
            throw new NotImplementedException();
        }

        private async Awaitable SwitchDay(int i)
        {
            var switchDay = new SwitchDay(switchDayWaitSeconds, i);
            await switchDay.Run();
        }

        private async Awaitable VisualNovel()
        {
            EventData visualNovelEvent = EventManager.GetValidEvent();
            
            var visualNovelPhase = new VisualNovelPhase(visualNovelEvent);
            await visualNovelPhase.Run();
        }

        private async Awaitable<bool> Management()
        {
            var managementPhase = new ManagementPhase();
            PhaseResult<bool> result = await managementPhase.Run();
            
            return result;
        }
    }
}