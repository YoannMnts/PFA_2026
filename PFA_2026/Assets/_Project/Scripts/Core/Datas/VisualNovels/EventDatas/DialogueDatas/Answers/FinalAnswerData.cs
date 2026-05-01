using System;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.DialogueLines;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers
{
    [CreateAssetMenu(fileName = "FinalAnswerData", menuName = "Naussilus/VisualNovel/Answer/FinalAnswerData", order = 0)]
    public class FinalAnswerData : AnswerData
    {
        [field : SerializeField]
        public DialogueLineData[] NpcText { get; private set; }
        
        [field : SerializeField]
        public ConditionalEffectData[] Effects { get; private set; }
        
        [field: SerializeField, HideInInspector]
        public string GUID { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GenerateNewGuid();
            }
            
#if UNITY_EDITOR
            string[] existings = AssetDatabase.FindAssets($"t:{nameof(FinalAnswerData)}");
            for (int i = 0; i < existings.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(existings[i]);
                var asset = AssetDatabase.LoadAssetAtPath<FinalAnswerData>(path);
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