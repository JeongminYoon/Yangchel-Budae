using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance = null;

    public GameObject       towerPrefab;
    public GameObject       nexusPrefab;
    public GameObject[]     towerParent = new GameObject[(int)Team.End];


    public GameObject[]     nexusList = new GameObject[(int)Team.End];
    public GameObject[,]    towerList = new GameObject[2, 2]; //[팀, 위치]
                                                           //팀 : 0 아군 / 1 적군
                                                           //위치 : 0 Left / 1 Right
    
    public void RemoveDeadTower(GameObject tower)
    {
        for (int i = 0; i < towerList.GetLength(0); ++i)
        {
            for (int k = 0; k < towerList.GetLength(1); ++k)
            {
                towerList[i,k] = null;
            }
        }
    }

    public GameObject TowerIsEnemySetting(GameObject enemyTower)
    {
        Units script = enemyTower.GetComponent<Units>();

        if (script != null)
        {
            script.IsEnemy = true;
        }

        return enemyTower;
    }

    public GameObject InstantiateTower(GameObject towerPrefab, GameObject parent, Vector3 pos, bool isEnemy = false)
    {
        Quaternion rotation = Quaternion.identity;

        if (isEnemy)
        { rotation = Quaternion.Euler(0f, 180f, 0f); }

        GameObject tower = Instantiate(towerPrefab, pos, rotation, parent.transform);

        if (isEnemy)
        {
            TowerIsEnemySetting(tower);
        }

        return tower;
    }
    public void TowerSetting()
    {
        bool isEnemy = false;
        GameObject temp;
        GameObject parentObj;
        GameObject prefab;

        for (int i = 0; i < Defines.towersPos.GetLength(0); ++i) //팀
        {
            for (int k = 0; k < Defines.towersPos.GetLength(1); ++k)//0,1=타워 / 2=넥서스
            {

                isEnemy = Funcs.I2B(i);
                parentObj = towerParent[i];
                if (k != 2)
                {//tower 
                    prefab = towerPrefab;
                }
                else
                {//Nexus
                    prefab = nexusPrefab;
                }

                temp = InstantiateTower(prefab, parentObj, Defines.towersPos[i, k], isEnemy);

                if (k != 2)
                {
                    towerList[i, k] = temp;
                }
                else { nexusList[i] = temp; }
            }
        }
    }

    //public GameObject SpawnTower(Vector3 pos, bool isEnemy)
    //{
        

        
    //}

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        TowerSetting();
    }

    // Update is called once per frame
    void Update()
    {

   
    }
}
