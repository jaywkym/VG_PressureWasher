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
    }

    public void getFinalScore(){
            scoreNumber = Mathf.RoundToInt(Score.instance.scoreNum);

            if (scoreNumber > highscore)
            {
                highscore = scoreNumber; // Update the highscore
                PlayerPrefs.SetInt("highscore", highscore);
                highScoreText.text = "Your Highscore: " + highscore.ToString(); // Update highscore text immediately
            }
            else
            {
                highScoreText.text = "Your Highscore: " + highscore.ToString(); // Keep the current highscore text
            }

            finalscore.text = scoreNumber.ToString();
        }

    // Update is called once per frame
    void Update()
    {
            //getFinalScore();
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
}
}

