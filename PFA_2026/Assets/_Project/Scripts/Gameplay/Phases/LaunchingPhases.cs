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
            for (int i = 0; i < MaxDay; i++)
            {
                var dialogue = new Dialogue();
                dialogue.Run();
                var decision = new Decision();
                decision.Run();
                var summary = new Summary();
                summary.Run();
            }
        }
    }
}