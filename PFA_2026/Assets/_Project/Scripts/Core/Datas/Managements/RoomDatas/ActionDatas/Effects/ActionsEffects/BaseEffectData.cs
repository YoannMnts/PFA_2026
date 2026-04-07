using System;
using Naussilus.Core.Datas.Conditions;
using Naussilus.Core.DatasOperators;
using UnityEditor;
using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    [CreateAssetMenu(fileName = "BaseEffectData", menuName = "Management/ActionEffect/BaseEffectData", order = 0)]
    public class BaseEffectData : ActionsEffect
    {
        [field: SerializeReference]
        public IOperand LeftOperand { get; private set; } 
        
        [field: SerializeField]
        public EMathOperator Operator { get; private set; }
        
        [field: SerializeReference]
        public IOperand RightOperand { get; private set; }
        
        [field: SerializeField]
        public ActionTarget StatTarget { get; private set; }
        
        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GenerateNewGuid();
            }
            
#if UNITY_EDITOR
            string[] existings = AssetDatabase.FindAssets($"t:{nameof(BaseEffectData)}");
            for (int i = 0; i < existings.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                var asset = AssetDatabase.LoadAssetAtPath<BaseEffectData>(path);
                if(asset != this && asset.GUID == GUID)
                    GenerateNewGuid();
            }
#endif
        }

        private void GenerateNewGuid()
        {
            GUID = Guid.NewGuid().ToString();
        }
    }
}