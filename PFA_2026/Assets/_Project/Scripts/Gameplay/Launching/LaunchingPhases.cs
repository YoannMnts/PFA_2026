using System;
using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naussilus.Gameplay
{
    public class LaunchingPhases : MonoBehaviour
    {
        [SerializeField] 
        private int maxDay;
        
        [SerializeField] 
        private int switchDayWaitSeconds;

        [SerializeField]
        private int defaultActionPoint;
        
        
        private void Start()
        {
            PhaseLifetime();
        }

        private async void PhaseLifetime()
        {
            try
            {
                //var intro = new IntroductionPhase();
                //await intro.Run();
                
                for (int i = 0; i < maxDay; i++)
                {
                    await SwitchDay(i);
                    
                    bool vnResult = await VisualNovel();
                    
                    await PlayerSwitch();
                    
                    bool mResult = await Management();
                    
                    await PlayerSwitch();
                    
                    if (!mResult || !vnResult)
                    {
                        GameOver();
                        break;
                    }
                }
                
                var ending = new EndingPhase(false);
                ending.RunAndForget();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void GameOver()
        {
            var ending = new EndingPhase(true);
            ending.RunAndForget();        
        }

        private async Awaitable PlayerSwitch()
        {
            EventManager.Init();
            var playerSwitch = new PlayerSwitch();
            await playerSwitch.Run();
        }

        private async Awaitable SwitchDay(int i)
        {
            var switchDay = new SwitchDay(switchDayWaitSeconds, i + 1);
            await switchDay.Run();
        }

        private async Awaitable<bool> VisualNovel()
        {
            var visualNovelEvent = EventManager.GetValidEvents();
            var visualNovelPhase = new VisualNovelPhase(visualNovelEvent);
            PhaseResult<bool> result = await visualNovelPhase.Run();
            
            return result;
        }

        private async Awaitable<bool> Management()
        {
            var managementPhase = new ManagementPhase(defaultActionPoint);
            PhaseResult<bool> result = await managementPhase.Run();
            
            return result;
        }
    }
}