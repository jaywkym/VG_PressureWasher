using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PressureWasher{
public class Player : MonoBehaviour
{
        // Start is called before the first frame update
        public float jetpackForce = 75.0f;
        private Rigidbody2D playerRigidbody;
        public float speed;
        public int playerScore;
        public float start;
        public float fowardMovementSpeed = 3.0f;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
        //need fixedupate with update bc they update at diff intervals
        void FixedUpdate()
        {
            bool jetpackActive = Input.GetButton("Fire1");
            if (jetpackActive)
            {
                playerRigidbody.AddForce(new Vector2(0, jetpackForce));
            }
            Vector2 newVelocity = playerRigidbody.velocity;
            newVelocity.x = fowardMovementSpeed;
            playerRigidbody.velocity = newVelocity;

        }

        // Update is called once per frame
        void Update()
    { 
        //MOVE
        if(Input.GetKey(KeyCode.D)){
            playerRigidbody.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
        }
        //CLEAN!!!!
        if(Input.GetKeyDown(KeyCode.Space)){
            playerRigidbody.AddRelativeForce(Vector2.up * speed * Time.deltaTime);
        }
        
    }

        //  COLLECT COINS

        private uint coins = 0; //store coin count
        public Text coinsCollectedLabel; //text label for coin score

        void collect(Collider2D coinCollider)
        {
            coins++;
            coinsCollectedLabel.text = coins.ToString();
            Destroy(coinCollider.gameObject);
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("coin"))
            {
                collect(collider);
            }



            //FOR other objects later like power ups, etc
            //else if
            //{
            //    HitByLaser(collider);
            //}

        }


    }
}

