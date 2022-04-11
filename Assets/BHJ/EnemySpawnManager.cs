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
        unitStatus = NewCardManager.instance.debugUnitDataList; //�ӽð�(����׿�) ���� ���Ӻ��嶧���� �� ���þ��� CardManager���� ���� �ҷ��ð�.
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
            print(unitStatus[rand].unitName + "��" + new Vector3(0, 0, 0) + "��ġ�� ��ȯ��");
            UnitFactory.instance.SpawnUnit((Enums.UnitClass)rand, new Vector3(0, 0, 0), true); //���⿡ Vector3���� ������ ��ġ�� �����ٰ�
            rand = Random.Range(0, 6); //�������� �ʱ�ȭ
    }

}
