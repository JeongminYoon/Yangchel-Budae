using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    // Start is called before the first frame update


   public float overLap = 5.0f;


    void Start()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, overLap);




      
        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].GetComponent<Units>() != null && colls[i].GetComponent<Units>().isEnemy == true)
            {
                colls[i].GetComponent<Units>().unitStatus.curHp -= 10;

                

            }






        }
        Destroy(this.gameObject);

    }

 

    




    }








