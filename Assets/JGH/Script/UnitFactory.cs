using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class UnitFactory : MonoBehaviour
{
    /// <singletone>
    public static UnitFactory instance = null;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
    }
    /// <singletone>

    public GameObject meleePrefab;
    public GameObject rangePrefab;
    public GameObject enemyPrefab;

    public bool Test()
    { 
        return true;
        

    }

    public GameObject SpawnUnit(UnitClass unitClass, Vector3 spawnPos, bool isEnemy = false)
    {
        GameObject spawnObj = null;

        switch (unitClass)
        {
            case UnitClass.melee:
                {
                    spawnObj = Instantiate(meleePrefab, spawnPos, Quaternion.identity);
                }
                break;

            case UnitClass.range:
                {
                    spawnObj = Instantiate(rangePrefab, spawnPos, Quaternion.identity);
                }
                break;

            case UnitClass.tanker:
                {

                }
                break;
            case UnitClass.healer:
                {

                }
                break;
            case UnitClass.skill:
                {

                }
                break;

            default:
                { }
                break;
        }

        //if (isEnemy)
        //{ UnitManager.instance.unitList[Defines.enemy].Add(spawnObj);  }
        //else { UnitManager.instance.unitList[Defines.ally].Add(spawnObj); }
        //void temp = isEnemy == true ? UnitManager.instance.enemyList.Add(spawnObj) : UnitManager.instance.allyList.Add(spawnObj);

        if (spawnObj != null)
        {
            spawnObj.GetComponent<Units>().IsEnemy = isEnemy;
            UnitManager.instance.unitList[Funcs.B2I(isEnemy)].Add(spawnObj);
        }
       
        return spawnObj;
    }

    public GameObject  SpawnMeleeUnit(Vector3 spawnPos)
    {
        GameObject tempObj = Instantiate(meleePrefab, spawnPos, Quaternion.identity);
        tempObj.GetComponent<Units>().IsEnemy = false;
        return tempObj;
    }

    public GameObject SpawnRangeUnit(Vector3 spawnPos)
    {
        GameObject tempObj = Instantiate(rangePrefab, spawnPos, Quaternion.identity);
        tempObj.GetComponent<Units>().IsEnemy = false;
        return tempObj;
    }

    public GameObject SpawnEnemy(Vector3 spawnPos)
    {
        GameObject tempObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        tempObj.GetComponent<Units>().IsEnemy = true;
        return tempObj;
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
