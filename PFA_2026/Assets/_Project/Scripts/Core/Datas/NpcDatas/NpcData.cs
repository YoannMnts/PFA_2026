using System;
using Naussilus.Core.Consequences;
using UnityEditor;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [CreateAssetMenu(fileName = "NpcData", menuName = "NpcData", order = 0)]
    public class NpcData : ScriptableObject
    {
        [field: SerializeField] 
        public string Name { get; private set; }

        [field: SerializeField] 
        public NpcBehavior[] Behavior { get; private set; }

        [field: SerializeField] 
        public NpcMentalState[] MentalState { get; private set; }

        [field: SerializeField] 
        public NpcRelationship[] Relationships { get; private set; }

        [field: SerializeField] 
        public EGender Gender { get; private set; }

        [field: SerializeField, TextArea] 
        public string CurrentThinking { get; private set; }

        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GenerateNewGuid();
            }

#if UNITY_EDITOR
            string[] existings = AssetDatabase.FindAssets($"t:{nameof(NpcData)}");
            for (int i = 0; i < existings.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                var asset = AssetDatabase.LoadAssetAtPath<NpcData>(path);
                if (asset != this && asset.GUID == GUID)
                    GenerateNewGuid();
            }
#endif
        }

        private void GenerateNewGuid()
        {
            GUID = Guid.NewGuid().ToString();
        }
    }

    [Serializable]
    public class NpcValue : IRelationshipValue, INpcSelector
    {
        [field: SerializeField]
        public NpcData NpcData { get; private set; }
    }
}