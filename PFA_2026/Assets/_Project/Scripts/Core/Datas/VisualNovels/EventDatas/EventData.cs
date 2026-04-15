using System;
using Naussilus.Core.NpcDatas;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas;
using UnityEditor;
using UnityEngine;

namespace Naussilus.Core.VisualNovels.EventDatas
{
    [CreateAssetMenu(fileName = "EventData", menuName = "VisualNovel/EventData", order = 0)]
    public class EventData : ScriptableObject
    {
        [field : SerializeField]
        public string Name { get; private set; }
        
        [field : SerializeField]
        public NpcData[] Npcs { get; private set; }
        
        [field : SerializeField]
        public int Priority { get; private set; }
        
        [field : SerializeField]
        public DialogueData FirstDialogue { get; private set; }
        
        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GenerateNewGuid();
            }
            
#if UNITY_EDITOR
            string[] existings = AssetDatabase.FindAssets($"t:{nameof(EventData)}");
            for (int i = 0; i < existings.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                var asset = AssetDatabase.LoadAssetAtPath<EventData>(path);
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