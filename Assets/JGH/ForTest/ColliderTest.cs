using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    public BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = this.gameObject.GetComponent<BoxCollider>();
            
    }

    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetKey(KeyCode.Space))
        {
            bc.enabled = false;
        }
        else
        {
            bc.enabled = true;
        }



        
    }
}
