using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using Naussilus.Core.Sounds;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SelectIncidentUI : MonoPhaseListener<SelectIncident>
    {
        [SerializeField] 
        private CanvasGroup group;
        
        [SerializeField] 
        private IncidentSlotUIList incidentSlotUIList;

        private SelectIncident currentPhase;
        
        public Incident[] CurrentIncidents => currentPhase.CurrentIncidents;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(SelectIncident phase)
        {
            if (currentPhase != null)
                return;
            
            currentPhase = phase;
            group.Show();
            incidentSlotUIList.Connect(CurrentIncidents);
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.VisualNovelMusic(true);
                SoundDesignManager.instance.PlaySound(SoundsEnum.Knock);
            }
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectIncident phase)
        {
            if (currentPhase == null)
                return;
            
            currentPhase = null;
            group.Hide();
            incidentSlotUIList.Disconnect();
            
            base.OnPhaseEnd(phase);
        }

        public void OnIncidentSelected(Incident incident)
        {
            currentPhase.SetResult(incident);
        }
    }
}