using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    static public HpBar instance = null;

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
        for (int i = 0; i < unitList.Count; i++)
        {
               hpBarList[i].transform.position = cam.WorldToScreenPoint(unitList[i].transform.position + new Vector3(0f, 1.2f, 0f));
        }

        if (Input.GetMouseButtonDown(1)) // �������� < �̰� ���� ��ȯ������, �ı������� �������� �ǰ� �ٲٸ� ��
        {//��ȯ���������� Card.cs�� Ʈ����, �ı����������� Hp�� �����ϸ鼭 ���ֿ��Լ� ��ӹ��� hp���� 0�� �Ǹ� ȣ���Ű�� �ϸ� �ǰڳ�
            //SearchUnit();
            if (unitStatusList[0] != null)
            { 
            print(unitStatusList[0].hp);
            }
        }
    }

    public void SearchUnit() 
    {
        unitList.Clear();
        unitStatusList.Clear();
        GameObject[] fieldUnit = GameObject.FindGameObjectsWithTag("Friendly");
        
        for (int i = 0; i < fieldUnit.Length;i++)
        {
            unitList.Add(fieldUnit[i]);
            GameObject hpBar = Instantiate(hpBarPrefab, fieldUnit[i].transform.position, Quaternion.identity, transform);
            hpBarList.Add(hpBar);

            unitStatusList.Add(unitList[i].GetComponent<Units>().unitStatus); //unitStatusList�� ���ָ���Ʈ�� �������ͽ����� �ְ����
        }
    }











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
