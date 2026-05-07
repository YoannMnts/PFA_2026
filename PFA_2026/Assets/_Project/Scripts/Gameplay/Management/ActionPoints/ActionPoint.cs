using UnityEngine;

namespace DefaultNamespace
{
    public class ActionPoint
    {
        public int CurrentAP { get; private set; }

        private readonly int defaultAP;
        
        public ActionPoint(int defaultValue)
        {
            CurrentAP = defaultValue;
            defaultAP = defaultValue;
        }

        public void ComputeAP(int newAP)
        {
            var oldAP = CurrentAP - newAP;
            CurrentAP = Mathf.Clamp(oldAP, 0, defaultAP);
        }
    }
}