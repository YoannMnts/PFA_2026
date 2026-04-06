using System;
using Naussilus.Core.Datas.Conditions;
using UnityEditor;
using UnityEngine;

namespace Naussilus.Core.Datas.VisualNovels
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "VisualNovel/DialogueData", order = 0)]
    public class DialogueData : ScriptableObject
    {
        [field : SerializeField]
        public ComplexCondition[] Conditions { get; private set; }
        
        [field : SerializeField]
        public DialogueLine Lines { get; private set; }
        
        [field : SerializeField]
        public Answer[] Answers { get; private set; }
        
        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GenerateNewGuid();
            }
            
#if UNITY_EDITOR
            string[] existings = AssetDatabase.FindAssets($"t:{nameof(DialogueData)}");
            for (int i = 0; i < existings.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                var asset = AssetDatabase.LoadAssetAtPath<DialogueData>(path);
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