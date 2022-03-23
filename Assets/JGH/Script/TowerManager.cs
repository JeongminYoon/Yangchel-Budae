using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance = null;



    public GameObject[,] towerList = new GameObject[2, 2]; //[팀, 위치]
                                                        //팀 : 0 아군 / 1 적군
                                                        //위치 : 0 Left / 1 Right
    public GameObject[] nexusList = new GameObject[(int)Team.End];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        towerList[Defines.enemy, Defines.left] = GameObject.Find("Enemy_LeftTower");
        towerList[Defines.enemy, Defines.right] = GameObject.Find("Enemy_RightTower");

        towerList[Defines.ally, Defines.left] = GameObject.Find("Ally_LeftTower");
        towerList[Defines.ally, Defines.right] = GameObject.Find("Ally_RightTower");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
