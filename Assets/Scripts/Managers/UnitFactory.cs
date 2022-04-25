using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class UnitFactory : MonoBehaviour
{
    /// <singletone>
    public static UnitFactory instance = null;

    public Material enemyMaterial;

    /// <singletone>


    public GameObject[] allyUnitPrefabs = new GameObject[(int)Enums.UnitClass.End];
    public GameObject[] enemyUnitPrefabs = new GameObject[(int)Enums.UnitClass.End];

    [HideInInspector]
    public GameObject[,] unitPrefabs = new GameObject[(int)Enums.Team.End, (int)Enums.UnitClass.End];


    public GameObject SpawnUnit(UnitClass unitClass, Vector3 spawnPos, bool isEnemy = false)
    {
        GameObject spawnObj = null;


        spawnObj = Instantiate(unitPrefabs[Funcs.B2I(isEnemy),(int)unitClass], spawnPos, Quaternion.identity);

        Units unitScript = spawnObj.GetComponent<Units>();

        #region switchCaseSpawn_DontUse
        //switch (unitClass)
        //{
        //    case UnitClass.melee:
        //        {
        //            spawnObj = Instantiate(meleePrefab, spawnPos, Quaternion.identity);
        //        }
        //        break;

        //    case UnitClass.melee2:
        //        {
        //            spawnObj = Instantiate(meleePrefab, spawnPos, Quaternion.identity);
        //        }
        //        break;

        //    case UnitClass.range:
        //        {
        //            spawnObj = Instantiate(rangePrefab, spawnPos, Quaternion.identity);
        //        }
        //        break;
        //    case UnitClass.range2:
        //        {
        //            spawnObj = Instantiate(rangePrefab, spawnPos, Quaternion.identity);
        //        }
        //        break;

        //    case UnitClass.tanker:
        //        {

        //        }
        //        break;
        //    case UnitClass.healer:
        //        {

        //        }
        //        break;

        //    default:
        //        { }
        //        break;
        //}
        #endregion


        if (spawnObj != null)
        {
            unitScript.isEnemy = isEnemy;

            if (isEnemy)
            {
                spawnObj.gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));
            }

            spawnObj.tag = "Unit";

            //hpbar 생성
            GameObject hpBarObj = HpBarManager.instance.HpBarInstrate();
			hpBarObj.GetComponent<HpBar>().Unit = spawnObj;
			UnitManager.instance.unitList[Funcs.B2I(isEnemy)].Add(spawnObj);
        }

			return spawnObj;
    }

    #region dontUse!
    //public GameObject  SpawnMeleeUnit(Vector3 spawnPos)
    //{
    //    GameObject tempObj = Instantiate(meleePrefab, spawnPos, Quaternion.identity);
    //    tempObj.GetComponent<Units>().IsEnemy = false;
    //    return tempObj;
    //}

    //public GameObject SpawnRangeUnit(Vector3 spawnPos)
    //{
    //    GameObject tempObj = Instantiate(rangePrefab, spawnPos, Quaternion.identity);
    //    tempObj.GetComponent<Units>().IsEnemy = false;
    //    return tempObj;
    //}

    //public GameObject SpawnEnemy(Vector3 spawnPos)
    //{
    //    GameObject tempObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    //    tempObj.GetComponent<Units>().IsEnemy = true;
    //    return tempObj;
    //}

    #endregion

    private void Awake()
    {
        if (instance == null)
        { instance = this; }

        for (int i = 0; i < allyUnitPrefabs.Length; ++i)
        {
            unitPrefabs[(int)Enums.Team.ally, i] = allyUnitPrefabs[i];
            unitPrefabs[(int)Enums.Team.enemy, i] = enemyUnitPrefabs[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < allyUnitPrefabs.Length; ++i)
        //{
        //    unitPrefabs[(int)Enums.Team.ally,i] = allyUnitPrefabs[i];
        //    unitPrefabs[(int)Enums.Team.enemy, i] = enemyUnitPrefabs[i];
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }
}
