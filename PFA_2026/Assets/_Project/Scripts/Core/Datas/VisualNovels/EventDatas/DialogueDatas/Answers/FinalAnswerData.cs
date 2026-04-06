using System;
using Naussilus.Core.Datas.Conditions;
using UnityEditor;
using UnityEngine;

namespace Naussilus.Core.Datas.VisualNovels
{
    [Serializable]
    public struct FinalAnswers
    {
        [field : SerializeField]
        public ComplexCondition[] Conditions { get; private set; }
        
        [field : SerializeField]
        public EventConsequence[] Consequence { get; private set; }
    }
    [CreateAssetMenu(fileName = "FinalAnswerData", menuName = "VisualNovel/Answer/FinalAnswerData", order = 0)]
    public class FinalAnswerData : Answer
    {
        [field : SerializeField]
        public FinalAnswers[] FinalAnswers { get; private set; }
        
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