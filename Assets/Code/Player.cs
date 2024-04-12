using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PressureWasher{
    public class Player : MonoBehaviour
    {
        public static Player instance;

        // Start is called before the first frame update
        public float jetpackForce = 40.0f;
        private Rigidbody2D playerRigidbody;
        public float speed;

        public float playerScore;
        //public Text scoreText;
        //public float pointsPerSec = 20;

        //MOVEMENT
        public float start;
        public float fowardMovementSpeed = 3.0f;

        //OBSTACLE GENERATION
        public Transform[] spawnPoints;
        public GameObject[] obstaclePrefabs;
        public float timeElapsed;
        public float obstacleDelay;

        public float maxObstacleDelay = 2f;
        public float minObstacleDelay = 0.5f;

        //GROUND CHECK
        public Transform groundCheckTransform;
        private bool isGrounded;
        public LayerMask groundCheckLayerMask;
        private Animator mouseAnimator;

        //JETPACK AND LIFE
        public ParticleSystem jetpack;
        private bool isDead = false;

        //MENU CONTROLLER
        public bool isPaused = false;

        // Audio clips
        public AudioClip coinCollectSound;
        public AudioSource jetpackAudio;
        public AudioSource footstepsAudio;

        void SpawnObstacle()
        {
            float spawnXPosition = transform.position.x + 18f;
            float spawnYPosition = Random.Range(-4.5f, 4.5f);
            Vector2 spawnPosition = new Vector2(spawnXPosition, spawnYPosition);

            int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject randomObstaclePrefabs = obstaclePrefabs[randomObstacleIndex];

            Instantiate(randomObstaclePrefabs, spawnPosition, Quaternion.identity);
        }

        IEnumerator ObstacleSpawnTimer()
        {
            yield return new WaitForSeconds(obstacleDelay);
            SpawnObstacle();
            StartCoroutine("ObstacleSpawnTimer");
        }

        void Start()
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);

            playerRigidbody = GetComponent<Rigidbody2D>();
            StartCoroutine("ObstacleSpawnTimer");
            mouseAnimator = GetComponent<Animator>();

        }
        //need fixedupate with update bc they update at diff intervals
        void FixedUpdate()
        {
            bool jetpackActive = Input.GetButton("Fire1");
            jetpackActive = jetpackActive && !isDead;

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
        }


        // Update is called once per frame
        void Update()
        {
            if(isPaused){
                return;
            }

            timeElapsed += Time.deltaTime;
            float decreaseDelayOverTime = maxObstacleDelay - ((maxObstacleDelay - minObstacleDelay) / 30f * timeElapsed);
            obstacleDelay = Mathf.Clamp(decreaseDelayOverTime, minObstacleDelay, maxObstacleDelay);

            //MOVE
            if (Input.GetKey(KeyCode.D))
            {
                playerRigidbody.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
            }
            //CLEAN!!!!
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.AddRelativeForce(Vector2.up * speed * Time.deltaTime);
            }

        }

        //  COLLECT COINS + SCORE

        public uint coins = 0; //store coin count
        public Text coinsCollectedLabel; //text label for coin score

        public float coinVal = 50;


        void collect(Collider2D coinCollider)
        {
            coins++;
            if (coins >= 30)
            {
                // Show revive button
                //GameOver.instance.ShowReviveButton();
            }
            AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);

            coinsCollectedLabel.text = coins.ToString();
            Score.instance.coinsCollected = coins;
            Score.instance.scoreNum += coinVal;
            Destroy(coinCollider.gameObject);
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("coin"))
            {
                collect(collider);
            }
            //HitByLaser(collider);
        }

        void UpdateGroundedStatus()
        {
            //1
            isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
            //2
            mouseAnimator.SetBool("isGrounded", isGrounded);
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

        void HitByLaser(Collider2D laserCollider)
        {
            isDead = true;
            mouseAnimator.SetBool("isDead", true);
            SceneManager.LoadScene("GameOver"); //load game over screen
        }

        void AdjustFootstepsAndJetpackSound(bool jetpackActive)
        {
            footstepsAudio.enabled = !isDead && isGrounded;
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

