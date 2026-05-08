using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ActionPoint
    {
        public event Action<int> OnAddOrRemove;
        
        public int Value { get; private set; }

        private readonly int defaultAP;
        
        public ActionPoint(int defaultValue)
        {
            Value = defaultValue;
            defaultAP = defaultValue;
        }

        public bool TryAddOrRemove(int newAP)
        {
            var computeAP = Value + newAP;
            if (computeAP < 0)
                return false;
            Debug.Log($"AddOrRemove - {computeAP}");
            Value = Mathf.Clamp(computeAP, 0, defaultAP);
            OnAddOrRemove?.Invoke(Value);
            return true;
        }
    }
}