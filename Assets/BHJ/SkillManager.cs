using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    /// <singletone>
    public static SkillManager instance = null;
    /// <singletone>
    public GameObject skill1Prefab;
    public GameObject skill2Prefab;

    public GameObject skill1;
    public GameObject skill2;


    public bool isSkill2Live = false;


    public void UseSkill1()
    { 
       skill1 = Instantiate(skill1Prefab);

       

    }


    public void UseSkill2()
    {

        skill2 = Instantiate(skill2Prefab);

        isSkill2Live = true;
                       
       

    



    }
  

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        




    }
}
