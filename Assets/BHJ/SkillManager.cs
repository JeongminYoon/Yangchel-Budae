using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    /// <singletone>
    public static SkillManager instance = null;
    /// <singletone>
    public GameObject skill1Prefab;


    public void UseSkill1()
    { 
        Instantiate(skill1Prefab);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSkill1();
        }
    }
}
