using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class DecisionUI : MonoBehaviour, IPhaseListener<Decision>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField] 
        private Transform root;
        
        [SerializeField] 
        private DecisionButton buttonPrefab;
        
        private void Start()
        {
            canvasGroup.Hide();
            foreach (Transform button in root)
            {
                Destroy(button.gameObject);
            }
        }
        
        private void OnEnable()
        {
            this.Register();
        }

        private void OnDisable()
        {
            this.Unregister();
        }
        
        public void OnPhaseBegin(Decision phase)
        {
            foreach (Transform button in root)
            {
                Destroy(button.gameObject);
            }
            for (int i = 0; i < phase.CurrentAnswers.Length; i++)
            {
                var decisionAnswer = Instantiate(buttonPrefab, root);
                //decisionAnswer.InitButton(i, phase.CurrentAnswers[i].ButtonText);
            }
            canvasGroup.Show();
        }

        public void OnPhaseEnd(Decision phase)
        {
            canvasGroup.Hide();
            foreach (Transform button in root)
            {
                Destroy(button.gameObject);
            }
        }
    }
}