using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace PressureWasher
{
    public class GameOver : MonoBehaviour
    {
        public TMP_Text finalscore;
        public int scoreNumber;
        public TMP_Text highScoreText;
        public TMP_Text coinScoreText;
        public float coinScore = 0;
        public int highscore = 0;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1f;
            highscore = PlayerPrefs.GetInt("highscore", 0);
            coinScore = PlayerPrefs.GetInt("coins", 0);
            UpdateHighscoreText();
        }

        public void getFinalScore()
        {
            scoreNumber = Mathf.RoundToInt(Score.instance.scoreNum);

            coinScore = Mathf.RoundToInt(Score.instance.coinsCollected * 50);

            if (scoreNumber > highscore)
            {
                highscore = scoreNumber;
                PlayerPrefs.SetInt("highscore", highscore);
                UpdateHighscoreText();
            }
            
            coinScoreText.text = "Coin Score: " + coinScore.ToString();
            finalscore.text = scoreNumber.ToString();
        }

        void UpdateHighscoreText()
        {
            highScoreText.text = "Your Highscore: " + highscore.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (finalscore != null)
            {
                getFinalScore();
            }
        }

        public void LoadGame()
        {
            Time.timeScale = 1f;
            int selectedBackground = PlayerPrefs.GetInt("selectedBackground");
            SceneManager.LoadScene(selectedBackground);
            //string levelName = "Level " + levelId;
            //SceneManager.LoadScene(levelName);
            //SceneManager.LoadScene("Level 1");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("StartGameScene");
        }

        // Method to reset highscore
        public void ResetHighscore()
        {
            highscore = 0;
            PlayerPrefs.SetInt("highscore", highscore);
            UpdateHighscoreText();
        }
    }
}

