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

    public GameObject[] towerWeaponPrefab = new GameObject[(int)Team.End];


    public GameObject[]     nexusList = new GameObject[(int)Team.End];
    public GameObject[,]    towerList = new GameObject[2, 2]; //[팀, 위치]
                                                              //팀 : 0 아군 / 1 적군
                                                              //위치 : 0 Left / 1 Right
    
    public Material enemyTowerMat;
    public Material enemyNexusMat;

    public void RemoveDeadTower(GameObject tower)
    {
        for (int i = 0; i < towerList.GetLength(0); ++i)
        {
            for (int k = 0; k < towerList.GetLength(1); ++k)
            {
                if (towerList[i, k] == tower)
                {
                    towerList[i, k] = null;
                }
            }
        }
    }

    public GameObject BuildingIsEnemySetting(GameObject enemyBuilding, bool isTower  = true )
    {
        Units script = enemyBuilding.GetComponent<Units>();

        if (script != null)
        {
            script.IsEnemy = true;
        }

        if (isTower)
        {
            if (enemyTowerMat != null)
            {
                Material[] matTemp = enemyBuilding.GetComponent<Renderer>().materials;
                matTemp[0] = enemyTowerMat;
                enemyBuilding.GetComponent<Renderer>().materials = matTemp;

            }
        }
        else
        {
            if (enemyNexusMat != null)
            {
                Material[] matTemp = enemyBuilding.GetComponent<Renderer>().materials;
                matTemp[0] = enemyNexusMat;
                enemyBuilding.GetComponent<Renderer>().materials = matTemp;
            }
        }


        return enemyBuilding;
    }

    public GameObject InstantiateBuilding(GameObject towerPrefab, GameObject parent, Vector3 pos, bool isTower  = true ,bool isEnemy = false)
    {
        Quaternion rotation = Quaternion.identity;

        if (isEnemy)
        { rotation = Quaternion.Euler(0f, 180f, 0f); }

        GameObject building = Instantiate(towerPrefab, pos, rotation, parent.transform);

        //hpBar
		GameObject hpBarObj = HpBarManager.instance.HpBarInstrate();
		hpBarObj.GetComponent<HpBar>().Unit = building;


		////머테리얼 바꾸는거
		//렌더러의 머테리얼즈(배열) 받아와서 수정한 뒤에 
		//다시 렌더러의 머테리얼즈(배열)에 삽입해야함.

		if (isEnemy)
        {
            BuildingIsEnemySetting(building, isTower);
        }

        return building;
    }
    public void TowerSetting()
    {
        bool isEnemy = false;
        GameObject temp;
        GameObject parentObj;
        GameObject prefab;
        bool isTower;
        for (int i = 0; i < Defines.towersPos.GetLength(0); ++i) //팀
        {
            for (int k = 0; k < Defines.towersPos.GetLength(1); ++k)//0,1=타워 / 2=넥서스
            {

                isEnemy = Funcs.I2B(i);
                parentObj = towerParent[i];
                if (k != 2)
                {//tower 
                    prefab = towerPrefab;
                    isTower = true;
                }
                else
                {//Nexus
                    prefab = nexusPrefab;
                    isTower = false;
                }

                temp = InstantiateBuilding(prefab, parentObj, Defines.towersPos[i, k], isTower, isEnemy);
                temp.GetComponent<Units>().isEnemy = isEnemy;

                if (k != 2)
                {//타워
                    temp.tag = "Tower";
                    towerList[i, k] = temp;
                }
                else 
                { //넥서스
                    temp.tag = "Nexus";
                    nexusList[i] = temp;
                }
            }
        }
    }

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
