using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naussilus.Gameplay
{
    public class MainMenuManager : MonoBehaviour
    {
        public void Play(int playerNumber)
        {
            SceneManager.LoadScene(1);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}