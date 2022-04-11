using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    #region singletone
    /// <singletone>
    static public EnemySpawnManager instance = null;
    /// <singletone>
    /// 

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
    int rand;

    // Start is called before the first frame update
    void Start()
    { // CardManager.unitStatusList = 전체 카드의 스테이터스 리스트. 
            unitStatus = NewCardManager.instance.debugUnitDataList; //디버그 유닛리스트. 실제 게임씬에서는 밑의값을 사용할것
        #region 실제 카드리스트
        //unitStatus = CardManager.instance.unitStatusList; //유닛스테이터스를 카드선택씬에있는 카드스테이터스 리스트를 불러옴
                                                          //ㄴ 근데 다른씬 로드되면 이전 스크립트는 파괴되는거 아니였나...? 왜 불러와지지 DontDestroyOnLoad 카드매니저에는 안썼는데
        #endregion
        rand = Random.Range(0, 6); //랜덤 적 유닛 지정
    }


    // Update is called once per frame
    void Update()
    {
        currentCost += Time.deltaTime / 2f; //현재 적의 코스트
        if (currentCost > 10f)
        {
            currentCost = 10f;
        }

        if (unitStatus[rand].cost <= currentCost) //랜덤한 적 유닛의 코스트가 현재 코스트보다 높으면
        {
            EnemyRandom(rand); //적 소환, 현재코스트 - 사용된 유닛 코스트
            rand = Random.Range(0, 6); //랜덤 적 유닛 재지정
            HpBarManager.instance.SearchUnit(); // HpBar생성 (정확히는 맵상 유닛이 뭐있는지 찾는거임)
        }
    }


    public void EnemyRandom(int rand)
    {
            currentCost -= unitStatus[rand].cost;
            print(unitStatus[rand].unitName + "이" + new Vector3(0, 0, 0) + "위치에 소환됨");
            UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, new Vector3(0, 0, 0), true); //여기에 Vector3값도 랜덤한 위치로 정해줄것
            rand = Random.Range(0, 6); //랜덤유닛 초기화
    }

}
