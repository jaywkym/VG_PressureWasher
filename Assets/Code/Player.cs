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
        private bool isDead = false;
        public Transform groundCheckTransform;
        private bool isGrounded;
        public LayerMask groundCheckLayerMask;
        //private Animator mouseAnimator;
        public ParticleSystem jetpack;
        public AudioClip coinCollectSound;
        public AudioSource jetpackAudio;


        void Start(){
        playerRigidbody = GetComponent<Rigidbody2D>();
        //mouseAnimator = GetComponent<Animator>();

        }
        //need fixedupate with update bc they update at diff intervals
        void FixedUpdate()
        {
            bool jetpackActive = Input.GetButton("Fire1");
            if (jetpackActive)
            {
                playerRigidbody.AddForce(new Vector2(0, jetpackForce));
            }
  
            if (!isDead)
            {
                Vector2 newVelocity = playerRigidbody.velocity;
                newVelocity.x = fowardMovementSpeed;
                playerRigidbody.velocity = newVelocity;
            }

            UpdateGroundedStatus();
            AdjustJetpack(jetpackActive);
            AdjustFootstepsAndJetpackSound(jetpackActive);


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


        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("coin"))
            {
                collect(collider);
                AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);

            }



            HitByLaser(collider);
            

        }


        void HitByLaser(Collider2D laserCollider)
        {
            isDead = true;
        }

        void UpdateGroundedStatus()
        {
            //1
            isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
            //2
            //mouseAnimator.SetBool("isGrounded", isGrounded);
        }

        void AdjustJetpack(bool jetpackActive)
        {
            var jetpackEmission = jetpack.emission;
            jetpackEmission.enabled = !isGrounded;
            if (jetpackActive)
            {
                jetpackEmission.rateOverTime = 300.0f;
            }
            else
            {
                jetpackEmission.rateOverTime = 75.0f;
            }
        }

        void HitByLaser()
        {
            if (!isDead)
            {
                //AudioSource laserZap = laserCollider.gameObject.GetComponent<AudioSource>();
                //laserZap.Play();
            }
        }

        void AdjustFootstepsAndJetpackSound(bool jetpackActive)
        { 
            jetpackAudio.enabled = !isDead && !isGrounded;
            if (jetpackActive)
            {
                jetpackAudio.volume = 1.0f;
            }
            else
            {
                jetpackAudio.volume = 0.5f;
            }
        }




    }
}

