using System;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.DialogueLines;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Naussilus.Core.VisualNovels.EventDatas.DialogueDatas
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "VisualNovel/DialogueData", order = 0)]
    public class DialogueData : ScriptableObject
    {
        [field : SerializeField]
        public DialogueLineData[] Lines { get; private set; }
        
        [field : SerializeField]
        public AnswerData[] Answers { get; private set; }
        
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