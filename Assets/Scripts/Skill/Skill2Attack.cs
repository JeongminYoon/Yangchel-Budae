using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Attack : MonoBehaviour
{
    public float overLap = 10.0f;
    float currrenTime = 0.0f;
   


    // Start is called before the first frame update
    void Start()
    {
  
       
    }

    void Update()
    {

        currrenTime += Time.deltaTime;

        if (currrenTime > 1)
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, overLap);

            for (int i = 0; i < colls.Length; i++)
            {

                if (colls[i].GetComponent<Units>() != null && colls[i].GetComponent<Units>().isEnemy == true)
                {
                   
                    colls[i].GetComponent<Units>().Hit(20);

                    currrenTime = 0.0f;


                }


            }
        }


    }

}

   