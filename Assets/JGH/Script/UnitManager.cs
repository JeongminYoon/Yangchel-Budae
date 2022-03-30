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

    public void RemoveDeadUnit(GameObject deadUnit)
    {
        for (int i = 0; i < unitList.Length; ++i)
        {
            for (int k = 0; k < unitList[i].Count; ++k)
            {
                if (deadUnit.GetComponent<Units>().unitStatus.isDead == true)
                {
                    unitList[i].Remove(deadUnit);
                }
            }
        }
    }

    public void ResearchTarget_AllUnit(GameObject deadUnit)
    {//죽는 유닛 자체를 인자로 넘어옴
        for (int i = 0; i < unitList.Length; ++i)
        {
            for (int k = 0; k < unitList[i].Count; ++k)
            {
                Units tempUnit = unitList[i][k].GetComponent<Units>();

                if (tempUnit != null && tempUnit.targetObj == deadUnit && tempUnit.gameObject.tag != "Tanker")
                {//유닛 리스트 다 돌면서 유닛이 null아니고 -> 안전장치
                    //targetObj가 방금 죽은 유닛일 경우
                     tempUnit.SearchUnit();
                }
            }
        }
    }

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
