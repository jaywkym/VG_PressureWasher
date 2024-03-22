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

    // Start is called before the first frame update
    void Start()
    {
    }

    public void getFinalScore(){
        scoreNumber = Mathf.RoundToInt(Score.instance.scoreNum);
        finalscore.text = scoreNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        getFinalScore();
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

