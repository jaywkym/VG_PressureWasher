using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PressureWasher{
    public class Score : MonoBehaviour
    {
        
    Text score;
    
    Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        player = gameObject.GetComponent<Player>();
        score.text = player.playerScore.ToString();
    }
    }
}

