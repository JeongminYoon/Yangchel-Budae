using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    #region singletone
    /// <singletone>
    static public EnemySpawnManager instance = null;
    /// <singletone>

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    List<UnitStatus> unitStatus;
    float currentCost = 0f;
    int rand;  // 유닛 소환 패턴1 랜덤값
    int rand1; // 패턴 1 2 정해주는 랜덤값
    
    int rand2;  // 유닛 소한 패턴2 랜덤값
    int rand3;  // 유닛 소환 패턴2 랜덤값


    int randomSpawn; //적 타워 앞 위치 3개 랜덤값
    int randomSpawn2; // 적 타워 1개 ,넥서스 랜덤값'



    // Start is called before the first frame update
    void Start()
    { // CardManager.unitStatusList = 전체 카드의 스테이터스 리스트. 

        #region Debug
       unitStatus = NewCardManager.instance.debugUnitDataList; //디버그 유닛리스트. 실제 게임씬에서는 밑의값을 사용할것
        #endregion

        #region Release
        //unitStatus = GameManager.MyHandsList; //유닛스테이터스를 카드선택씬에있는 카드스테이터스 리스트를 불러옴
                                                              //ㄴ 근데 다른씬 로드되면 이전 스크립트는 파괴되는거 아니였나...? 왜 불러와지지 DontDestroyOnLoad 카드매니저에는 안썼는데
        #endregion

        rand = Random.Range(0, 6); //랜덤 적 유닛 지정
        
      
        rand2 = Random.Range(0, 6);
        rand3 = Random.Range(0, 6);




    }
   
    // Update is called once per frame
    void Update()
    {
        currentCost += Time.deltaTime / 4f; //현재 적의 코스트
        if (currentCost > 10f)
        {
            currentCost = 10f;
        }

        if (unitStatus[rand].cost <= currentCost) //랜덤한 적 유닛의 코스트가 현재 코스트보다 높으면
        {
            EnemyRandom(rand); //적 소환, 현재코스트 - 사용된 유닛 코스트
            rand = Random.Range(0, 6); //랜덤 적 유닛 재지정
        }

        if (unitStatus[rand2].cost + unitStatus[rand3].cost <= currentCost)
        {
            EnemyRandom(rand);


            rand = Random.Range(0, 6);


        }
        rand1 = Random.Range(0, 2);


        //if (Input.GetKeyDown(KeyCode.F1))
        //{

        //    TowerManager.instance.towerList[1, 0].GetComponent<Units>().unitStatus.curHp = 0;




        //}


        //if (Input.GetKeyDown(KeyCode.F2))
        //{

        //    TowerManager.instance.towerList[1, 1].GetComponent<Units>().unitStatus.curHp = 0;

        //}

        //Destroy(TowerManager.instance.towerList[1, 0]);

        //Destroy(TowerManager.instance.towerList[1, 1]);

    }


    Vector3 leftTowerPos;
    Vector3 rightTowerPos;
    Vector3 nexusTowerPos;



    void EnemyRandomPatton1()

   {


        if (TowerManager.instance.towerList[1, 0] == null && TowerManager.instance.towerList[1, 1] == null)
        {
            //둘다 없을때


            UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);

        }

        if (TowerManager.instance.towerList[1, 0] == null && TowerManager.instance.towerList[1, 1])
        {
            //적 왼쪽타워가 없고 오른쪽 타워만 존재할때
            randomSpawn = Random.Range(0, 2);


            if (randomSpawn == 0)
            {
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, rightTowerPos, true);


            }
            else if (randomSpawn == 1)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);



            }


        }

        if (TowerManager.instance.towerList[1, 0] && TowerManager.instance.towerList[1, 1] == null)
        {
            //적 오른쪽 타워가 없고 왼쪽 타워만 존재할때
            randomSpawn = Random.Range(0, 2);
            if (randomSpawn == 0)
            {
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, leftTowerPos, true);


            }
            else if (randomSpawn == 1)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);
            }


        }


        if (TowerManager.instance.towerList[1, 0] && TowerManager.instance.towerList[1, 1])
        {
            //적 양쪽 타워가 다 존재할때
            randomSpawn = Random.Range(0, 3);


            if (randomSpawn == 0)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, leftTowerPos, true);

            }
            else if (randomSpawn == 1)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, rightTowerPos, true);
            }
            else if (randomSpawn == 2)
            {


                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);




            }

        }

    }

   
    void EnemyRandomPatton2()
    {


        if (TowerManager.instance.towerList[1, 0] == null && TowerManager.instance.towerList[1, 1])
        {
            //적 왼쪽타워가 없고 오른쪽 타워만 존재할때
            randomSpawn2 = Random.Range(0, 3);


            
      
            if(randomSpawn2 == 0)
            {


                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, rightTowerPos, true);

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);




            }
            if(randomSpawn2 == 1)
            {
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, rightTowerPos, true);

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, rightTowerPos, true);


            }
            if(randomSpawn2 == 2)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);

            }
         


            


        }



        if (TowerManager.instance.towerList[1, 0] && TowerManager.instance.towerList[1, 1] == null)
        {
            //적 오른쪽 타워가 없고 왼쪽 타워만 존재할때


            randomSpawn2 = Random.Range(0, 2);
            

          
            if(randomSpawn2 == 0)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, leftTowerPos, true);
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);




            }
            if(randomSpawn2 == 1)
            {
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, leftTowerPos, true);

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, leftTowerPos, true);


            }
            
            if(randomSpawn2 == 2)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);

            }





        }



        if (TowerManager.instance.towerList[1, 0] && TowerManager.instance.towerList[1, 1])
        {
            //적 양쪽 타워가 다 존재할때
            randomSpawn = Random.Range(0, 6);


            if (randomSpawn == 0)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, leftTowerPos, true);
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, rightTowerPos, true);
            }
            else if (randomSpawn == 1)
            {



                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, rightTowerPos, true);
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);

            }
            else if (randomSpawn == 2)
            {


                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, leftTowerPos, true);



            }
            else if (randomSpawn == 3)
            {
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, leftTowerPos, true);
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, leftTowerPos, true);


            }
            else if (randomSpawn == 4)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, rightTowerPos, true);
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, rightTowerPos, true);

            }
            else if(randomSpawn == 5)
            {

                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, nexusTowerPos, true);


            }






        }



    }
  



   



    public void EnemyRandom(int rand)
    {
        // 적 타워 밑 넥서스 포지션 받아옴


        leftTowerPos = Defines.enemyTower_LeftPos +new Vector3(0, 0, -4);

        rightTowerPos = Defines.enemyTower_RightPos + new Vector3(0, 0, -4);

        nexusTowerPos = Defines.enemyNexusPos + new Vector3(0, 0, -4);

        


        currentCost -= unitStatus[rand].cost;
        print(unitStatus[rand].unitName + "이" + new Vector3(0, 0, 0) + "위치에 소환됨");




       
        if(rand1 == 0) // 패턴 1 ( 무난한 유닛 1마리씩 소환 )
        {


            EnemyRandomPatton1();


        }
        
       if(rand1 == 1)    // 패턴2 유닛 2마리씩 소환    
        {


            EnemyRandomPatton2();




        }








      


      







    }



        
        

    }
    
         
          
    


