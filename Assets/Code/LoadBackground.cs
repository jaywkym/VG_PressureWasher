using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBackground : MonoBehaviour
{

    public GameObject[] backgroundPrefabs;
    public Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        int selectedBackground = PlayerPrefs.GetInt("selectedBackground");
        GameObject prefab = backgroundPrefabs[selectedBackground];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

    }

  
}
