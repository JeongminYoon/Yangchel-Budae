using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
   public bool pb = true;

    public GameObject bulletPrefab;

    public float speed = 6.0f;

    public float bulletCreateTime = 1.0f;

    public GameObject[] spwanPos /*= new GameObject[3]*/;
    public Transform spawnPosLoc;
    void Start()
    {

         
    }



    float currentTime = 0.0f;
    // Update is called once per frame
    void Update()
    {
       


    
        if (pb == true)
        {
       
            
            //�ð����
            currentTime += Time.deltaTime;
            if(currentTime  > bulletCreateTime)
            {
                //�Ѿ��� ������
                GameObject bullet = Instantiate(bulletPrefab);

                // bullet.transform.position = transform.position;
                //  bullet.transform.position = new Vector3(Random.Range(0.0f, 16.0f) - 8.0f, 5.0f, Random.Range(0.0f, 30.0f)-15.0f);

                for (int i = 0; i < spwanPos.Length; i++)
                {
                    spawnPosLoc.position = transform.position;
                }

                int nRand = Random.Range(0, spwanPos.Length);
                bullet.transform.position =  spwanPos[nRand].transform.position;


              currentTime = 0.0f;
            }
            

            transform.position += speed * Vector3.forward * Time.deltaTime;

        }


        if (transform.position.z >= 20.0f)
            
        {

            Destroy(gameObject);
            


        }
    }

    int MyRandom(int min , int max)
    {
        return Random.Range(min, max-1);
    }
}
