using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PressureWasher
{
    public class Score : MonoBehaviour
    {
        public static Score instance;

        public Transform player;
        public Text scoreText;

        public float scoreNum;
        public float pointsPerSec = 20;
        public uint coinsCollected;


        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }


        // Update is called once per frame
        void Update()
        {
            scoreNum += (pointsPerSec * Time.deltaTime);
            //Debug.Log(pointsPerSec * Time.deltaTime);
            scoreText.text = "Score: " + scoreNum.ToString("0");
        }
    }
}
