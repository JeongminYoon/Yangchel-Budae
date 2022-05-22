using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<GameObject> GetUnitList_Val(int team = Defines.ally)
    {
        return unitList[team].ToList<GameObject>();
    }
    public List<GameObject> GetUnitList_Ref(int team = Defines.ally)
    {
        return unitList[team];
    }

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
        //22 04 27 유닛 죽으면 걔를 타겟으로 잡고 있는 애들한테 null처리 하라고 요청하고
        //리서치 들어갈 예rm
        //따로 함수만들면 시간복잡도 ^2 더 늘어 나니까 한 곳에서 처리 ㄱㄱㄱㄱ

        for (int i = 0; i < unitList.Length; ++i)
        {
            for (int k = 0; k < unitList[i].Count; ++k)
            {
                Units tempUnit = unitList[i][k].GetComponent<Units>();

                if (tempUnit != null && tempUnit.targetObj == deadUnit/* && tempUnit.gameObject.tag != "Tanker"*/)
                {//유닛 리스트 다 돌면서 유닛이 null아니고 -> 안전장치
                    //targetObj가 방금 죽은 유닛일 경우

                    if (tempUnit.gameObject.GetComponent<Units>().unitStatus.unitNum == (int)Enums.UnitClass.medic)
                    {
                        tempUnit.DeadTargetException(deadUnit);
                        (tempUnit as MedicFunc).SearchUnit();
                    }
                    else 
                    {
                        tempUnit.DeadTargetException(deadUnit);
                        tempUnit.SearchUnit();
                    }
                    
                }
            }
        }
    }


    public void GameEnd(bool isNexusEnemy)
    {
        //적팀 넥서스가 부서졌을 경우 -> 게임 승리 WIN
        //true일 때
        //우리팀은 victory 호출
        //적팀은 Defeated 호출

        Debug.Log(unitList[Defines.enemy].Count);

        foreach (GameObject unit in unitList[Funcs.B2I(isNexusEnemy)])
        {
            Units unitScript = unit.GetComponent<Units>();
            if (unitScript != null)
            {
                unitScript.navAgent.isStopped = true;
                unitScript.Defeated();
            }
        }

        foreach (GameObject unit in unitList[Funcs.B2I(!isNexusEnemy)])
        {
            Units unitScript = unit.GetComponent<Units>();
            if (unitScript != null)
            {
                unitScript.navAgent.isStopped = true;
                unitScript.Victory();
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
        Funcs.GameManagerHasNotExist();
    }

    void Update()
    {
        
    }
}
