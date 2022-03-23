using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class UnitManager : MonoBehaviour
{
    /// <singletone>
    public static UnitManager instance = null;
    /// <singletone>

    public List<GameObject>[] unitList = new List<GameObject>[(int)Team.End];
   
    //public List<GameObject>[] unitList;
       //팀 : 0 아군 / 1 적군


    void Awake()
	{
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < unitList.Length; ++i)
        {
            unitList[i] = new List<GameObject>();
        }
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
