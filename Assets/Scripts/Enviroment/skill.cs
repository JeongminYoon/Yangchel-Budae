using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
   public bool pb = false;

    Transform tr ;
    void Start()
    {

         tr = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("介之中今中之今中之");

        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{

        //    Debug.Log("揚楝颶 六六");
        //    pb = true;
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pb = true;
        }

        if (pb == true)
        {
            tr.position += (3 * new Vector3(2.0f, 0.0f, 0.0f) * Time.deltaTime);

        }


        if (tr.position.x >= 10.0f)
            
        {

            Destroy(gameObject);
            


        }
    }
}
