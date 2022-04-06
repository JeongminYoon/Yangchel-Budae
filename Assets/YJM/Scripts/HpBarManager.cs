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

    Text hpText;

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
        if (Input.GetMouseButtonDown(1)) // �������� < �̰� ���� ��ȯ������, �ı������� �������� �ǰ� �ٲٸ� ��
        {//��ȯ���������� Card.cs�� Ʈ����, �ı����������� Hp�� �����ϸ鼭 ���ֿ��Լ� ��ӹ��� hp���� 0�� �Ǹ� ȣ���Ű�� �ϸ� �ǰڳ�
            //SearchUnit();
            if (unitStatusList[0] != null)
            { 
                print(unitStatusList[0].curHp);
            }
        }

        print(SkillManager.instance.isSkill2Live);
    }

    public void SearchUnit()
    {
        unitList.Clear();
        unitStatusList.Clear();
        List<GameObject> fieldUnit = UnitManager.instance.unitList[(int)Enums.Team.ally];
        List<GameObject> enemyfieldUnit = UnitManager.instance.unitList[(int)Enums.Team.enemy];
        //loadUnitList(fieldUnit);

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

    void loadUnitList(List<GameObject> a)
    {
        a.Add(TowerManager.instance.towerList[0, 0]);
        a.Add(TowerManager.instance.towerList[0, 1]);
        a.Add(TowerManager.instance.towerList[1, 0]);
        a.Add(TowerManager.instance.towerList[1, 1]);
        a.Add(TowerManager.instance.nexusList[0]);
        a.Add(TowerManager.instance.nexusList[1]);
    }
    void HpBarWork()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            hpBarList[i].transform.position = cam.WorldToScreenPoint(unitList[i].transform.position + new Vector3(0f, 1.2f, 0f));
            hpBarList[i].GetComponent<Image>().fillAmount = unitStatusList[i].curHp / unitStatusList[i].fullHp;
            hpText = hpBarList[i].gameObject.transform.Find("HpText").gameObject.GetComponent<Text>();
            hpText.text = (unitStatusList[i].curHp + "/" + unitStatusList[i].fullHp).ToString();
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
