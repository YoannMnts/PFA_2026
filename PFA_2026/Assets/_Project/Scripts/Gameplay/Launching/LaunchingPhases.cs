using System;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using Naussilus.Gameplay.VisualNovel;
using UnityEngine;

namespace Naussilus.Gameplay.Launcher
{
    public class LaunchingPhases : MonoBehaviour
    {
        [SerializeField] 
        private int maxDay;
        
        [SerializeField] 
        private int switchDayWaitSeconds;

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
                    await VisualNovel();
                    await PlayerSwitch();
                    bool result = await Management();
                    await PlayerSwitch();
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

        private static async Awaitable PlayerSwitch()
        {
            var playerSwitch = new PlayerSwitch();
            await playerSwitch.Run();
        }

        private async Awaitable SwitchDay(int i)
        {
            var switchDay = new SwitchDay(switchDayWaitSeconds, i);
            await switchDay.Run();
        }

        private async Awaitable VisualNovel()
        {
            Incident visualNovelEvent = EventManager.GetValidEvent();
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