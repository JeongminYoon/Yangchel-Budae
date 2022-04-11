using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
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

        List<UnitStatus> unitStatus;
    float currentCost = 0f;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        // print(NewCardManager.instance.debugUnitDataList.Count);
        unitStatus = NewCardManager.instance.debugUnitDataList; //임시값(디버그용) 실제 게임빌드때에는 덱 선택씬의 CardManager에서 값을 불러올것.
        rand = Random.Range(0, 6);
    }






    // Update is called once per frame
    void Update()
    {
        currentCost += Time.deltaTime / 2f;
        if (currentCost > 10f)
        {
            currentCost = 10f;
        }
        if (unitStatus[rand].cost <= currentCost)
        {
            EnemyRandom(rand);
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
