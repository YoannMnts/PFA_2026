using System;
using UnityEditor;
using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "Management/RoomData", order = 0)]
    public class RoomData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public string Description { get; private set; }
        
        [field: SerializeField]
        public ActionData[] Actions { get; private set; }
        
        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GenerateNewGuid();
            }
            
#if UNITY_EDITOR
            string[] existings = AssetDatabase.FindAssets($"t:{nameof(RoomData)}");
            for (int i = 0; i < existings.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                var asset = AssetDatabase.LoadAssetAtPath<RoomData>(path);
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