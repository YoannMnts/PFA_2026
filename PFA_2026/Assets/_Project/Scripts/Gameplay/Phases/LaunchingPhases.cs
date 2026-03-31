using System;
using System.Threading.Tasks;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay.Scripts
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
            for (int i = 0; i < maxDay; i++)
            {
                var switchDay = new SwitchDay(switchDayWaitSeconds);
                await switchDay.Run();
                await VisualNovelPhase();
                await SideViewPhases();
            }
        }

        private static async Awaitable VisualNovelPhase()
        {
            var dialogue = new Dialogue();
            await dialogue.Run();
            var decision = new Decision();
            await decision.Run();
            var summary = new Summary();
            await summary.Run();
        }

        private static async Awaitable SideViewPhases()
        {
            var sideView = new SideView();
            await sideView.Run();
        }
    }
}