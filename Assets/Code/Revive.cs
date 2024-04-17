using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace PressureWasher
{
    public class Revive : MonoBehaviour
    {
        public GameObject reviveUI;
        public GameObject player;

        public static Revive instance;

        private bool revived;

        void Awake()
        {
            instance = this; 
        }

        public void YouWantToRevive()
        {
            if (revived)
            {
                SceneManager.LoadScene("GameOver");
                reviveUI.SetActive(false);
            }
            else //if revived is false
            {
                reviveUI.SetActive(true);
            }
        }
    }
}
