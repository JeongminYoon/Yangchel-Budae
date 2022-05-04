using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public UnitStatus skillStatus_Origin;
    private UnitStatus skillStatus;
    
    public GameObject towerPrefab;
    
    public GameObject tower;

    float currentTime = 0.0f;

    float currentTime2 = 0.0f;

    public GameObject skill2Effect1;
    public GameObject skill2Effect2;
    public GameObject skill2Effect3;

    Vector3 allyNexusPos;

    int rand1;
    


    void Start()
    {
        // allyNexusPos = Defines.allyNexusPos + new Vector3(0, 0, +6);


        //  Vector3 towerPos = allyNexusPos;


        towerPrefab.transform.position = Defines.allyNexusPos + new Vector3(0, 0, 6);


        skillStatus = ScriptableObject.CreateInstance<UnitStatus>();
        skillStatus.DeepCopy(skillStatus_Origin);



      





    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        currentTime2 += Time.deltaTime;

        if (currentTime > 10.0f)
        {


            SkillManager.instance.isSkill2Live = false;


            Destroy(tower);

            Destroy(gameObject);





            currentTime = 0.0f;




        }



        rand1 = Random.Range(0, 3);


        skill2Effect1.transform.position = towerPrefab.transform.position + new Vector3(0, 0, 8);


        skill2Effect2.transform.position = towerPrefab.transform.position + new Vector3(-6, 0, 8);

        skill2Effect3.transform.position = towerPrefab.transform.position + new Vector3(6, 0, 8);



        if (currentTime2 > 0.3)
        {


            if(rand1 == 0)
            {


                Instantiate(skill2Effect1);

                currentTime2 = 0.0f;

            }

            if(rand1 == 1)
            {


                Instantiate(skill2Effect2);
                currentTime2 = 0.0f;

            }
            if(rand1 == 2)
            {


                Instantiate(skill2Effect3);
                currentTime2 = 0.0f;
            }

            
          


        }

        


    }
}
