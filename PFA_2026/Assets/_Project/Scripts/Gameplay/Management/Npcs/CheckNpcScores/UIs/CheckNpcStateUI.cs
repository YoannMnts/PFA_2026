using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Gameplay.Behaviors;
using Naussilus.Gameplay.MentalStates;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class CheckNpcStateUI : MonoPhaseListener<CheckNpcState>
    {
        private CheckNpcState current;

        [SerializeField] private CanvasGroup group;
        [SerializeField] private TMP_Text titleName;
        [SerializeField] private BehaviorUIList behaviorUIList;
        [SerializeField] private MentalStateUIList mentalStateUIList;
        [SerializeField] private RelationshipUIList relationshipUIList;
        [SerializeField] private Button relationshipButton;
        [SerializeField] private Button statButton;


        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(CheckNpcState phase)
        {
            titleName.text = phase.CurrentNpc.Name;
            current = phase;
            group.Show();
            //behaviorUIList.Connect(phase.NpcBehaviors);
            mentalStateUIList.Connect(phase.NpcMentalStates);
            relationshipButton.onClick.AddListener(ChangeToRelationship);
            statButton.onClick.AddListener(ChangeToStat);
        
        
            if (this.TryGetService(out PlayerController controller))
                controller.PlayerInteractions.CanInteract.AddPriority(this, PriorityTags.Default, false);
        
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(CheckNpcState phase)
        {
            current = null;
            //behaviorUIList.Disconnect();
            mentalStateUIList.Disconnect();
            group.Hide();
            relationshipButton.onClick.RemoveAllListeners();
            statButton.onClick.RemoveAllListeners();
            
            if (this.TryGetService(out PlayerController controller))
                controller.PlayerInteractions.CanInteract.RemovePriority(this);
        
            base.OnPhaseEnd(phase);
        }

        private void ChangeToStat()
        {
            relationshipUIList.Disconnect();
            mentalStateUIList.Connect(current.NpcMentalStates);
        }

        private void ChangeToRelationship()
        {
            mentalStateUIList.Disconnect();
            relationshipUIList.Connect(current.NpcRelationships);
        }

        public void Cancel()
        {
            if (current != null)
                current.SetResult(false);
        }
    }
}