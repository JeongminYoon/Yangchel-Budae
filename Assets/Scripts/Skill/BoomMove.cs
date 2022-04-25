using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomMove : MonoBehaviour
{

    float speed = 10.0f;
    
    public GameObject boomPrefab;

    bool bb = false;



    private void OnTriggerEnter(Collider other)
    {
        if(bb == false)
        {
            bb = true;

            Instantiate(boomPrefab);


        }

        Destroy(this.gameObject);

    }



    void Start()
    {



        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Vector3.down * Time.deltaTime;

    }
}
