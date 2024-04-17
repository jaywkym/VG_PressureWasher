using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchCharacterScript : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level 1");


    }

    //public GameObject avatar1, avatar2;

    //int whichAvatarIsOn = 1;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    avatar1.gameObject.SetActive(true);
    //    avatar2.gameObject.SetActive(false);
    //}

    //public void SwitchAvatar()
    //{
    //    switch (whichAvatarIsOn) {
    //        case 1:
    //            whichAvatarIsOn = 2;
    //            avatar1.gameObject.SetActive(false);
    //            avatar2.gameObject.SetActive(true);
    //            break;

    //        case 2:
    //            whichAvatarIsOn = 1;
    //            avatar1.gameObject.SetActive(true);
    //            avatar2.gameObject.SetActive(false);
    //            break;
    //    }
    //}

}
