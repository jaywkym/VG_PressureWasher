using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace PressureWasher{
public class GameOver : MonoBehaviour
{
        public TMP_Text finalscore;
        public int scoreNumber;
        public TMP_Text highScoreText;

        public int highscore = 0;

        // Start is called before the first frame update
        void Start()
        {
            highscore = PlayerPrefs.GetInt("highscore", 0);
            UpdateHighscoreText();
        }

        public void getFinalScore()
        {
            scoreNumber = Mathf.RoundToInt(Score.instance.scoreNum);

            if (scoreNumber > highscore)
            {
                highscore = scoreNumber;
                PlayerPrefs.SetInt("highscore", highscore);
                UpdateHighscoreText();
            }

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
            SceneManager.LoadScene("Level 1");
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

