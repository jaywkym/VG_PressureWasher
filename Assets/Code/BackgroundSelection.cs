using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSelection : MonoBehaviour
{
    public GameObject[] backgrounds;
    public int selectedBackground = 0;

    public void NextBackground()
    {
        backgrounds[selectedBackground].SetActive(false);
        selectedBackground = (selectedBackground + 1) % backgrounds.Length;
        backgrounds[selectedBackground].SetActive(true);
    }

    public void PreviousBackground()
    {
        backgrounds[selectedBackground].SetActive(false);
        selectedBackground--;
        if(selectedBackground < 0)
        {
            selectedBackground += backgrounds.Length;
        }
        backgrounds[selectedBackground].SetActive(true);
    }

    public void Update()
    {
        PlayerPrefs.SetInt("selectedBackground", selectedBackground);
        //SceneManager.LoadScene(selectedBackground);
    }
    //public void StartGame()
    //{
    //    if(selectedBackground == 1)
    //    {
    //        SceneManager.LoadScene(1);
    //    }

    //    if (selectedBackground == 2)
    //    {
    //        SceneManager.LoadScene(2);
    //    }

    //    if (selectedBackground == 3)
    //    {
    //        SceneManager.LoadScene(3);
    //    }





    //    //PlayerPrefs.SetInt("selectedBackground", selectedBackground);
    //    //SceneManager.LoadScene(selectedBackground);
    //}
}
