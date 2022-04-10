using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUintCall : MonoBehaviour
{
    // Start is called before the first frame update


    float currentTime = 0.0f;
   


    void Start()
    {


        Invoke("EnemyRandom", 3.0f);



    }




    void EnemyRandom()
    {




        int Rand = Random.Range(0, 8);

       



        UnitFactory.instance.SpawnUnit((Enums.UnitClass)Rand, new Vector3(0, 0, 0), true);
        {




        }



    }







    // Update is called once per frame
    void Update()
    {




        



    }
}
