using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressureWasher{
public class Player : MonoBehaviour
{
        // Start is called before the first frame update
        public float jetpackForce = 75.0f;
        private Rigidbody2D playerRigidbody;
        public float speed;

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

        }

        // Update is called once per frame
        void Update()
    {
        //CLEAN!!!!
        if(Input.GetKey(KeyCode.Space)){
            playerRigidbody.AddRelativeForce(Vector2.up * speed * Time.deltaTime);
        }
        
    }
}
}

