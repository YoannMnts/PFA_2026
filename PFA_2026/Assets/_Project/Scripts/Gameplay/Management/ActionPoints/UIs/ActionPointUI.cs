using Helteix.Tools.Phases.Listeners;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ActionPointUI : MonoPhaseListener<ManagementPhase>
    {
        [SerializeField]
        private TMP_Text actionPointText;

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            actionPointText.text = phase.CurrentActionPoint.Value.ToString();
            phase.CurrentActionPoint.OnAddOrRemove += OnAddOrRemove;
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            phase.CurrentActionPoint.OnAddOrRemove -= OnAddOrRemove;
            base.OnPhaseEnd(phase);
        }

        private void OnAddOrRemove(int value)
        {
            actionPointText.text = value.ToString();
        }
    }
}