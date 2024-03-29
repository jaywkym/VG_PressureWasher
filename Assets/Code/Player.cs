using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PressureWasher{
public class Player : MonoBehaviour
{
        // Start is called before the first frame update
        public float jetpackForce = 50.0f;
        private Rigidbody2D playerRigidbody;
        public float speed;
        public int playerScore;
        public float start;
        public float fowardMovementSpeed = 3.0f;

        public Transform[] spawnPoints;
        public GameObject[] obstaclePrefabs;
        public float timeElapsed;
        public float obstacleDelay;

        public float maxObstacleDelay = 2f;
        public float minObstacleDelay = 0.5f;

    void SpawnObstacle(){
        float spawnXPosition = transform.position.x + 18f;
        float spawnYPosition = Random.Range(-4.5f, 4.5f);
        Vector2 spawnPosition = new Vector2(spawnXPosition, spawnYPosition);

        int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject randomObstaclePrefabs = obstaclePrefabs[randomObstacleIndex];

        Instantiate(randomObstaclePrefabs, spawnPosition, Quaternion.identity);
    }

    IEnumerator ObstacleSpawnTimer(){
        yield return new WaitForSeconds(obstacleDelay);

        SpawnObstacle();

        StartCoroutine("ObstacleSpawnTimer");
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine("ObstacleSpawnTimer");
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
        timeElapsed += Time.deltaTime;

        float decreaseDelayOverTime = maxObstacleDelay - ((maxObstacleDelay - minObstacleDelay) / 30f * timeElapsed);
        obstacleDelay = Mathf.Clamp(decreaseDelayOverTime, minObstacleDelay, maxObstacleDelay);

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
        }


    }
}

