using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomMove : MonoBehaviour
{
   

    public GameObject boomPrefab;

    private void OnTriggerEnter(Collider other)
    {


        Destroy(gameObject);

        Instantiate(boomPrefab, transform.position, Quaternion.identity);
        

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
