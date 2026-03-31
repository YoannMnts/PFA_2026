using System;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay.Scripts
{
    public class LaunchingPhases : MonoBehaviour
    {
        [field: SerializeField]
        public int MaxDay { get; private set; }
        private void Start()
        {
            Lifetime();
        }

        private async void Lifetime()
        {
            for (int i = 0; i < MaxDay; i++)
            {
                var switchDay = new SwitchDay();
                await switchDay.Run();
                var dialogue = new Dialogue();
                await dialogue.Run();
                var decision = new Decision();
                await decision.Run();
                var summary = new Summary();
                await summary.Run();
            }
        }
    }
}