using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float speed = 10.0f;

    

    private void OnTriggerEnter(Collider other)
    {


        Destroy(gameObject);
                
        
        

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += speed * Vector3.down * Time.deltaTime;

    }
}
