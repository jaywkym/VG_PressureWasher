using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace PressureWasher
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;
        public GameObject mainMenu;

        void Awake()
        {
            instance = this;
            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            if (Player.instance != null)
            {
                Player.instance.isPaused = false;
            }
        }

        public void Show()
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(true);
            //Time.timeScale = 0;
            Player.instance.isPaused = true;
        }

        public void Play()
        {
            mainMenu.SetActive(false);
            Hide();
        }

        public void QuitGame()
        {
            SceneManager.LoadScene("StartGameScene");
        }

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            //if(Input.GetKeyDown(KeyCode.Escape)){
            //    MenuController.instance.Show();
            //}
        }
    }
}

