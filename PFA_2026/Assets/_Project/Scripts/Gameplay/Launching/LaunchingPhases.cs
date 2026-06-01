using System;
using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using Naussilus.Gameplay.RoleSelections;
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
            ConditionalEffectManager.ValidConsequences.Clear();
            PhaseLifecycle();
        }

        private async void PhaseLifecycle()
        {
            try
            {
                var roleSelection = new RoleSelection();
                await roleSelection.Run();
                
                for (int i = 0; i < maxDay; i++)
                {
                    await SwitchDay(i);
                    
                    bool visualNovelResult = await VisualNovel(i);
                    
                    await PlayerSwitch();
                    
                    bool managementResult = await Management();
                    
                    await PlayerSwitch();
                }
                
                var ending = new EndingPhase(false);
                ending.RunAndForget();
                
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
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
            var reelCurrentDay = currentDay + 1;
            var visualNovelEvent = EventManager.GetValidEvents(reelCurrentDay);
            var visualNovelPhase = new VisualNovelPhase(visualNovelEvent, reelCurrentDay);
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