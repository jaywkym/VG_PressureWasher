using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressureWasher
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject[] targetObject;
        private float distanceToTarget;
        


        // Start is called before the first frame update
        public void Start()
        {
            int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
            distanceToTarget = transform.position.x - targetObject[selectedCharacter].transform.position.x;
        }

        // Update is called once per frame
        public void Update()
        {
            int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
            float targetObjectX = targetObject[selectedCharacter].transform.position.x;
            Vector3 newCameraPosition = transform.position;
            newCameraPosition.x = targetObjectX + distanceToTarget;
            transform.position = newCameraPosition;
        }
    }
}
