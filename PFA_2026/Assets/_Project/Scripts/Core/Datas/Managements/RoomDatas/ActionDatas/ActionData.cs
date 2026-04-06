using System;
using UnityEditor;
using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    [CreateAssetMenu(fileName = "ActionData", menuName = "Management/ActionData", order = 0)]
    public class ActionData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public int Cost { get; private set; }
        
        [field: SerializeField]
        public Category[] Categories { get; private set; }
        
        [field: SerializeField]
        public Effect Effects { get; private set; }
        
        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GenerateNewGuid();
            }
            
#if UNITY_EDITOR
            string[] existings = AssetDatabase.FindAssets($"t:{nameof(ActionData)}");
            for (int i = 0; i < existings.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                var asset = AssetDatabase.LoadAssetAtPath<ActionData>(path);
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