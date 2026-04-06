using System;
using Naussilus.Core.Datas.Conditions;
using UnityEditor;
using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    [CreateAssetMenu(fileName = "ComplexEffectData", menuName = "Management/ActionEffect/ComplexEffectData", order = 0)]
    public class ComplexEffectData : ActionsEffect
    {
        [field: SerializeField]
        public ComplexCondition[] Conditions { get; private set; }
        
        [field: SerializeField]
        public BaseEffectData Effect { get; private set; }
        
        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GenerateNewGuid();
            }
            
#if UNITY_EDITOR
            string[] existings = AssetDatabase.FindAssets($"t:{nameof(ComplexEffectData)}");
            for (int i = 0; i < existings.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                var asset = AssetDatabase.LoadAssetAtPath<ComplexEffectData>(path);
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