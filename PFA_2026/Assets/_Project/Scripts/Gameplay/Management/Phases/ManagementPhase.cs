using System;
using System.Threading;
using Helteix.Tools.Phases;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class ManagementPhase : PhaseCompletionSource<bool>
    {
        public event Action<Room> OnRoomSelected;
    
        public ActionPoint CurrentActionPoint { get; private set; }
        public Npc[] CurrentNpcs { get; private set; }
    
        private readonly int defaultAP;
        private readonly int timerDuration;
        public ManagementPhase(int defaultAPValue, int timerDuration)
        {
            defaultAP = defaultAPValue;
            CurrentNpcs = NpcManager.GetAllNpcs();
            this.timerDuration = timerDuration;
        }
    
        protected override Awaitable Initialize(CancellationToken token)
        {
            CurrentActionPoint = new ActionPoint(defaultAP);
            var timer = new TimerPhase(this, timerDuration);
            timer.RunAndForget();
            RoomManager.SubtractAllCountdown();
            return base.Initialize(token);
        }

        protected override Awaitable Dispose(CancellationToken token)
        {
            ConditionalEffectManager.ComputeScheduledEffects();
            return base.Dispose(token);
        }

        public void SelectRoom(Room room)
        {
            OnRoomSelected?.Invoke(room);
        }
    }
}