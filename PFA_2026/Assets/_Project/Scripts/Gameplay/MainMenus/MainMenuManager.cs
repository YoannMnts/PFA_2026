using System;
using System.Collections;
using Naussilus.Gameplay.Buttons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naussilus.Gameplay
{
    public class MainMenuManager : MonoBehaviour
    {
        
        [SerializeField] GameObject CreditsMenu;

        private void Awake()
        {
            CreditsMenu.SetActive(false);
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.MenuMusic();
            }
        }

        public void Play(int playerNumber)
        {
            StartCoroutine(Launch(1));
        }
        
        public void Credits(bool openCredits)
        {
            if (openCredits)
            {
                StartCoroutine(Launch(2));
            }
            else
            {
                StartCoroutine(Launch(3));
            }
            
        }
        public void Exit()
        {
            StartCoroutine(Launch(4));
        }

        public void Feedback(GameObject buttonClicked)
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(buttonClicked);
            }
        }

        
        IEnumerator Launch(int sceneCode)
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                yield return new WaitForSeconds(0.3f);            
            }
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