using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PressureWasher{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync(1);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

