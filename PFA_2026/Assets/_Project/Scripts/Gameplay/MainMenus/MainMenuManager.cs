using System.Collections;
using Naussilus.Gameplay.Buttons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naussilus.Gameplay
{
    public class MainMenuManager : MonoBehaviour
    {
        
        [SerializeField] GameObject CreditsMenu;
        
        public void Play(int playerNumber, GameObject buttonClicked)
        {
            StartCoroutine(Launch(1,buttonClicked));
        }
        
        public void Credits(GameObject buttonClicked, bool openCredits)
        {
            if (openCredits)
            {
                StartCoroutine(Launch(2,buttonClicked));
            }
            else
            {
                StartCoroutine(Launch(3,buttonClicked));
            }
            
        }
        public void Exit(GameObject buttonClicked)
        {
            StartCoroutine(Launch(4,buttonClicked));
        }

        
        IEnumerator Launch(int sceneCode, GameObject buttonClicked)
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(buttonClicked);
            }
            yield return new WaitForSeconds(0.3f);
            if (sceneCode == 1)
            {
                SceneManager.LoadScene(1);
            }
            else if (sceneCode == 2)
            {
                CreditsMenu.SetActive(true);
            }
            else if (sceneCode == 3)
            {
                CreditsMenu.SetActive(false);
            }
            else if (sceneCode == 4)
            {
                Application.Quit();
            }
        }

        
    }
}