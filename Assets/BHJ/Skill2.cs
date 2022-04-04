using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public UnitStatus skillStatus_Origin;
    private UnitStatus skillStatus;
    
    public GameObject towerPrefab;
    
    GameObject tower;

    float currentTime = 0.0f;


    


    void Start()
    {//

        Vector3 towerPos = new Vector3(0.0f, 1.0f, -7.0f);


        skillStatus = ScriptableObject.CreateInstance<UnitStatus>();
        skillStatus.DeepCopy(skillStatus_Origin);

        tower = Instantiate(towerPrefab, towerPos, Quaternion.identity);





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
