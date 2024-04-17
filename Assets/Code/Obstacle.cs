using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PressureWasher{
    public class Obstacle : MonoBehaviour
    {
       

        void OnCollisionEnter2D(Collision2D other)
        {
            //Reload only when colliding with player
            if (other.gameObject.GetComponent<Player>())
            {
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //SceneManager.LoadScene("GameOver"); //load game over screen

                // Activate the revive panel instead of directly loading the Game Over scene
                Player.instance.isPaused = true;
                Time.timeScale = 0;
                Player.instance.revivePanel.SetActive(true);
            }
        }


    }
}

