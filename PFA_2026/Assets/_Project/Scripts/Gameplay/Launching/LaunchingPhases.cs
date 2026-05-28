using System;
using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
        
        [SerializeField]
        private int timerDuration;
        
        
        private void Start()
        {
            PhaseLifetime();
        }

        private async void PhaseLifetime()
        {
            try
            {
                //TODO faire l'intro
                //var intro = new IntroductionPhase();
                //await intro.Run();
                
                for (int i = 0; i < maxDay; i++)
                {
                    await SwitchDay(i);
                    
                    bool visualNovelResult = await VisualNovel(i);
                    
                    await PlayerSwitch();
                    
                    bool managementResult = await Management();
                    
                    await PlayerSwitch();
                    
                    if (!managementResult || !visualNovelResult)
                    {
                        GameOver();
                        break;
                    }
                }
                
                //TODO remettre l'ending
                /*
                var ending = new EndingPhase(false);
                ending.RunAndForget();
                */
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void GameOver()
        {
            //TODO remettre l'ending
            /*
            var ending = new EndingPhase(true);
            ending.RunAndForget();
            */
            SceneManager.LoadScene(0);
        }

        private async Awaitable PlayerSwitch()
        {
            EventManager.Init();
            var playerSwitch = new PlayerSwitch();
            await playerSwitch.Run();
        }

        private async Awaitable SwitchDay(int i)
        {
            var switchDay = new SwitchDay(switchDayWaitSeconds, i + 1, maxDay);
            await switchDay.Run();
        }

        private async Awaitable<bool> VisualNovel(int currentDay)
        {
            var visualNovelEvent = EventManager.GetValidEvents(currentDay);
            var visualNovelPhase = new VisualNovelPhase(visualNovelEvent);
            PhaseResult<bool> result = await visualNovelPhase.Run();
            
            return result;
        }

        private async Awaitable<bool> Management()
        {
            var managementPhase = new ManagementPhase(defaultActionPoint, timerDuration);
            PhaseResult<bool> result = await managementPhase.Run();
            
            return result;
        }
    }
}