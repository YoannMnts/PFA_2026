using System;
using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using Naussilus.Gameplay.VisualNovel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naussilus.Gameplay.Launcher
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
            NpcManager.Init();
            EventManager.Init();
            RoomManager.Init();
            PhaseLifetime();
        }

        private async void PhaseLifetime()
        {
            try
            {
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
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void GameOver()
        {
            SceneManager.LoadScene(0);
        }

        private async Awaitable PlayerSwitch()
        {
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
            var visualNovelEvent = EventManager.GetValidEvent();
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