using System;
using Naussilus.Core.Managements;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Naussilus.Core.NpcDatas
{
    [CreateAssetMenu(fileName = "NpcData", menuName = "Naussilus/NpcData/Npc", order = 0)]
    public class NpcData : ScriptableObject
    {
        [field: SerializeField] 
        public string Name { get; private set; }

        [field: SerializeField] 
        public BehaviorValueData[] Behavior { get; private set; }

        [field: SerializeField] 
        public MentalStateValueData[] MentalState { get; private set; }

        [field: SerializeField] 
        public NpcRelationshipData[] Relationships { get; private set; }

        [field: SerializeField] 
        public EGender Gender { get; private set; }
        
        [field: SerializeField]
        public Sprite DefaultEventSprite { get; private set; }
        
        [field: SerializeField]
        public Sprite DefaultIcon { get; private set; }

        [field: SerializeField]
        public RoomSlotPositionData[] DefaultPositions { get; private set; }
        
        [field: SerializeField, TextArea] 
        public string CurrentThinking { get; private set; }

        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
#if UNITY_EDITOR
            EditorApplication.delayCall += () =>
            {
                if (this == null) return;
        
                if (string.IsNullOrEmpty(GUID))
                    GenerateNewGuid();

                string[] existings = AssetDatabase.FindAssets($"t:{nameof(NpcData)}");
                for (int i = 0; i < existings.Length; i++)
                {
                    var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                    var asset = AssetDatabase.LoadAssetAtPath<NpcData>(path);
                    if (asset != this && asset.GUID == GUID)
                        GenerateNewGuid();
                }
            };
#endif
        }

        private void GenerateNewGuid()
        {
            GUID = Guid.NewGuid().ToString();
        }
    }
}