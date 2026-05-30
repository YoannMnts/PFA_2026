using System;
using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
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
            DefaultSlotManager.Init();
            NpcManager.Init();
            EventManager.Init();
            RoomManager.Init();
            PhaseLifecycle();
        }

        private async void PhaseLifecycle()
        {
            try
            {
                var intro = new IntroductionPhase();
                await intro.Run();
                
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
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.ManagmentMusic(false);
                SoundDesignManager.instance.VisualNovelMusic(true);
            }
            PhaseResult<bool> result = await visualNovelPhase.Run();
            
            return result;
        }

        private async Awaitable<bool> Management()
        {
            var managementPhase = new ManagementPhase(defaultActionPoint, timerDuration);
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.VisualNovelMusic(false);
                SoundDesignManager.instance.ManagmentMusic(true);
            }
            PhaseResult<bool> result = await managementPhase.Run();
            
            return result;
        }
    }
}