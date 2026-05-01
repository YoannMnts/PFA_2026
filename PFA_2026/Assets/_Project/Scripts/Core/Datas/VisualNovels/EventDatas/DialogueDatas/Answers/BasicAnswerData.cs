using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers
{
    [CreateAssetMenu(fileName = "BasicAnswerData", menuName = "Naussilus/VisualNovel/Answer/BasicAnswerData", order = 0)]
    public class BasicAnswerData : AnswerData
    {
        [field : SerializeField]
        public DialogueData NextDialogue { get; private set; }
        
        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GenerateNewGuid();
            }
            
#if UNITY_EDITOR
            string[] existings = AssetDatabase.FindAssets($"t:{nameof(BasicAnswerData)}");
            for (int i = 0; i < existings.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                var asset = AssetDatabase.LoadAssetAtPath<BasicAnswerData>(path);
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