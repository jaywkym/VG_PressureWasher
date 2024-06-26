using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PressureWasher{
    public class Player : MonoBehaviour
    {
        public static Player instance;
        public GameObject consumePrefab;

        // Start is called before the first frame update
        float jetpackForce = 40.0f;
        Rigidbody2D playerRigidbody;

        //public float playerScore;

        //REVIVAL
        public GameObject revivePanel;
        public uint coinsRequiredForRevive = 30;
        public GameObject notEnoughCoinsText;
        public bool revived = false; // Track if the player has been revived once

        //MOVEMENT
        public float start;
        public float fowardMovementSpeed = 3.0f;
        public KeyCode keySpace;
        public KeyCode keyEsc;



        //OBSTACLE GENERATION
        public Transform[] spawnPoints;
        public GameObject[] obstaclePrefabs;
        public float timeElapsed;
        public float obstacleDelay;

        public float maxObstacleDelay = 2.0f;
        public float minObstacleDelay = 0.7f;

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

        //OBSTACLE GENERATION
        void SpawnObstacle()
        {
            float spawnXPosition = transform.position.x + 18f;
            float spawnYPosition = Random.Range(-4.5f, 4.5f);
            Vector2 spawnPosition = new Vector2(spawnXPosition, spawnYPosition);

            foreach (Vector3 coinPosition in GenerateCoins.activeCoinPositions)
            {
            if (Vector2.Distance(spawnPosition, coinPosition) < 3.0f) // Assuming 1.0f as minimum non-colliding distance
                return; // Skip spawning if too close to a coin
            }
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

        //OBSTACLE SPAWN
        void Start()
        {
            Time.timeScale = 1f;
            instance = this;
            //DontDestroyOnLoad(this.gameObject);

            playerRigidbody = GetComponent<Rigidbody2D>();
            StartCoroutine("ObstacleSpawnTimer");
            mouseAnimator = GetComponent<Animator>();

            revivePanel.SetActive(false);
        }

        //need fixedupate with update bc they update at diff intervals
        void FixedUpdate()
        {
            bool jetpackActive = Input.GetKey(keySpace);
             
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
            if (Input.GetKeyDown(keyEsc))
            {
                MenuController.instance.Show();
            }

            if (isPaused){
                return;
            }

            timeElapsed += Time.deltaTime;
            float decreaseDelayOverTime = maxObstacleDelay - ((maxObstacleDelay - minObstacleDelay) / 30f * timeElapsed);
            obstacleDelay = Mathf.Clamp(decreaseDelayOverTime, minObstacleDelay, maxObstacleDelay);

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
                GameObject consume = Instantiate(
                Player.instance.consumePrefab, collider.transform.position, Quaternion.identity
                );
                Destroy(consume, 0.75f);
            }
        }

        void UpdateGroundedStatus()
        {
            //1
            isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
            //2
            mouseAnimator.SetBool("isGrounded", isGrounded);
        }

        //JETPACK ADJUST

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

        //REVIVAL
        public void Revive()
        {
            if (coins >= coinsRequiredForRevive)
            {
                //unpause the game
                Time.timeScale = 1;
                if (Player.instance != null)
                {
                    Player.instance.isPaused = false;
                }

                // Deduct coins for revival
                coins -= coinsRequiredForRevive;
                coinsCollectedLabel.text = coins.ToString(); // Update UI

                // Move the player forward by 20 units on the x-axis while keeping the y position unchanged
                Vector3 currentPosition = transform.position;
                currentPosition.x += 2.5f;
                transform.position = currentPosition;

                // Set revived flag to true
                revived = true;

                // Hide the revive panel
                if (revivePanel != null)
                {
                    revivePanel.SetActive(false);
                }
            }
            else //if not enough coins
            {
                // Display "Not Enough Coins" message in the revive panel
                if (revivePanel != null)
                {
                    Text revivePanelText = revivePanel.GetComponentInChildren<Text>();
                    if (revivePanelText != null)
                    {
                        revivePanelText.text = "Not Enough Coins";
                    }

                    // Grey out the "Yes" button and make it non-interactable
                    Button yesButton = revivePanel.GetComponentInChildren<Button>();
                    if (yesButton != null)
                    {
                        yesButton.interactable = false;
                        notEnoughCoinsText.SetActive(true);
                    }
                    
                    // Activate the revive panel
                    revivePanel.SetActive(true);
                }
            }

            // Pause or unpause the game based on whether the revive panel is active
            Time.timeScale = revivePanel.activeSelf ? 0 : 1;
            Player.instance.isPaused = revivePanel.activeSelf;
        }


        public void LoadGameOverScene()
        {
            SceneManager.LoadScene("GameOver");
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

