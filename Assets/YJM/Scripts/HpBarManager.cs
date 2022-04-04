using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour
{
    static public HpBarManager instance = null;

    [SerializeField] GameObject hpBarPrefab;
    List<Transform> fieldUnitLocation = new List<Transform>();

    List<GameObject> hpBarList = new List<GameObject>();
    List<GameObject> unitList = new List<GameObject>();
    List<UnitStatus> unitStatusList = new List<UnitStatus>();
    UnitStatus unitStatus;

    Camera cam = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        cam = Camera.main;
        SearchUnit();
    }

    // Update is called once per frame
    void Update()
    {
        HpBarWork();    
<<<<<<< HEAD:Assets/YJM/Scripts/HpBarManager.cs
 
=======
        if (Input.GetMouseButtonDown(1)) // �������� < �̰� ���� ��ȯ������, �ı������� �������� �ǰ� �ٲٸ� ��
        {//��ȯ���������� Card.cs�� Ʈ����, �ı����������� Hp�� �����ϸ鼭 ���ֿ��Լ� ��ӹ��� hp���� 0�� �Ǹ� ȣ���Ű�� �ϸ� �ǰڳ�
            //SearchUnit();
            if (unitStatusList[0] != null)
            { 
                print(unitStatusList[0].curHp);
            }
        }
>>>>>>> f68ff8f9cdadd206e6ebe145f926d56366f45b64:Assets/YJM/Scripts/HpBar.cs
    }

    public void SearchUnit()
    {
        unitList.Clear();
        unitStatusList.Clear();
        List<GameObject> fieldUnit = UnitManager.instance.unitList[(int)Enums.Team.ally];
        List<GameObject> enemyfieldUnit = UnitManager.instance.unitList[(int)Enums.Team.enemy];
        for (int i = 0; i < enemyfieldUnit.Count; i++)
        {
            fieldUnit.Add(enemyfieldUnit[i]);
        }

        for (int i = 0; i < fieldUnit.Count; i++)
        {
            unitList.Add(fieldUnit[i]);
            GameObject hpBar = Instantiate(hpBarPrefab, fieldUnit[i].transform.position, Quaternion.identity, transform);
            hpBarList.Add(hpBar);
            unitStatusList.Add(unitList[i].GetComponent<Units>().unitStatus); //unitStatusList�� ���ָ���Ʈ�� �������ͽ����� �ְ����
        }
    }

    void HpBarWork()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            hpBarList[i].transform.position = cam.WorldToScreenPoint(unitList[i].transform.position + new Vector3(0f, 1.2f, 0f));
<<<<<<< HEAD:Assets/YJM/Scripts/HpBarManager.cs
            hpBarList[i].GetComponent<Image>().fillAmount = unitStatusList[i].curHp / unitStatusList[i].fullHp;
=======
            hpBarList[i].GetComponent<Image>().fillAmount = unitStatusList[i].curHp / 100f; // ����������. unitStatusList[i].hp ���� 0~1�� ǥ���ؾ� �ϴµ� �������? ��ũ���ͺ� ������Ʈ���� �ҷ��;��ϳ� �ƴ� ����hp���� ��� ���� �����ؾ��ϳ�
>>>>>>> f68ff8f9cdadd206e6ebe145f926d56366f45b64:Assets/YJM/Scripts/HpBar.cs
            if (unitStatusList[i].curHp <= 0f)
            {
                SearchUnit();
            }

        }
    }


    //������ ��ȯ�Ǽ� �������ͽ� ��ũ���ͺ� ������Ʈ���� �ޱ����� SearchUnit�� �Ǿ������ ������ �־���, �������ͽ� ��ũ���ͺ� ������Ʈ���� Awake()���� �޾ƿ��°ɷ� �ٲ㼭 �ذ�������
    //���� ������ ����ٸ� ���ָ���Ʈ �������ͽ� ���� �� ��ü�� �ҷ��� ������ �ִ°� �ƴ϶� �����ʿ� ��ũ��Ʈ�� �־ ü���� �ٲ𶧶����, ������Ʈ���� ��� �����°ɷ� �ٲ����








    //[SerializeField] GameObject hpBarPrefab;
    //List<Transform> fieldUnitLocation = new List<Transform>();
    //List<GameObject> hpBarList = new List<GameObject>();

    //Camera cam = null;

    //void Start()
    //{
    //    cam = Camera.main;
    //    SearchUnit();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    for (int i = 0; i < fieldUnitLocation.Count; i++)
    //    {
    //        hpBarList[i].transform.position = cam.WorldToScreenPoint(fieldUnitLocation[i].transform.position + new Vector3(0f, 1.2f, 0f));
    //    }
    //}

    //void SearchUnit()
    //{
    //    GameObject[] fieldUnit = GameObject.FindGameObjectsWithTag("Friendly");
    //    for (int i = 0; i < fieldUnit.Length; i++)
    //    {
    //        fieldUnitLocation.Add(fieldUnit[i].transform);
    //        GameObject hpBar = Instantiate(hpBarPrefab, fieldUnit[i].transform.position, Quaternion.identity, transform);
    //        hpBarList.Add(hpBar);
    //    }
    ////}
}
