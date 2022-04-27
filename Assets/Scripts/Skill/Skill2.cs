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



    Vector3 allyNexusPos;


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

        if (currentTime > 10.0f)
        {


            SkillManager.instance.isSkill2Live = false;


            Destroy(tower);

            Destroy(gameObject);





            currentTime = 0.0f;




        }




    }
}
