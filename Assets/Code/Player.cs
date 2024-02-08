using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressureWasher{
public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //CLEAN!!!!
        if(Input.GetKey(KeyCode.Space)){
            _rb.AddRelativeForce(Vector2.up * speed * Time.deltaTime);
        }
        
    }
}
}

