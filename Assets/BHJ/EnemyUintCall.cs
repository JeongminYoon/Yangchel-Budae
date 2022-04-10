using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUintCall : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeLeft = 10.0f;
    private float nextTime = 0.0f;
   

    void Start()
    {
      


        



         




    }




   public void EnemyRandom()
    {




        int Rand = Random.Range(0, 8);

       



        UnitFactory.instance.SpawnUnit((Enums.UnitClass)Rand, new Vector3(0, 0, 0), true);
        {




        }



    }







    // Update is called once per frame
    void Update()
    {
        
      

            EnemyRandom();

            
        




    }
}
