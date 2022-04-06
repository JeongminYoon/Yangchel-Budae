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
        AddUnitList(fieldUnit, (int)Enums.Team.ally);
        List<GameObject> enemyfieldUnit = UnitManager.instance.unitList[(int)Enums.Team.enemy];
        AddUnitList(enemyfieldUnit, (int)Enums.Team.enemy);
        AddTowerList(unitList);

        for (int i = 0; i < unitList.Count; i++)
        {
            GameObject hpBar = Instantiate(hpBarPrefab, unitList[i].transform.position, Quaternion.identity, transform);
            hpBarList.Add(hpBar);
            unitStatusList.Add(unitList[i].GetComponent<Units>().unitStatus);
        }
    }

    void AddUnitList(List<GameObject> a, int b)
    {
        a = UnitManager.instance.unitList[b];
        for (int i = 0; i < a.Count; i++)
        {
            unitList.Add(a[i]);
        }

    }
    void AddTowerList(List<GameObject> a)
    {
        a.Add(TowerManager.instance.towerList[(int)Enums.Team.ally,0]);
        a.Add(TowerManager.instance.towerList[(int)Enums.Team.ally,1]);
        a.Add(TowerManager.instance.towerList[(int)Enums.Team.enemy, 0]);
        a.Add(TowerManager.instance.towerList[(int)Enums.Team.enemy, 1]);
        //a.Add(TowerManager.instance.nexusList[(int)Enums.Team.ally]);  //�̰Ŵ� �߰��ϸ� ������. ����????
        //a.Add(TowerManager.instance.nexusList[(int)Enums.Team.enemy]);
    }
    void HpBarWork()
    {
        for (int i = 0; i < unitList.Count; i++) // ����: HpBar�� �����ϰ��ִ� ������ ������ ������. 
        {
            if (unitStatusList[i].isDead != true) // �ϴ� ���ܹ��༭ ũ��Ƽ���� �����ߴ°� ����
            {
                hpBarList[i].transform.position = cam.WorldToScreenPoint(unitList[i].transform.position + new Vector3(0f, 1.2f, 0f));
                hpBarList[i].GetComponent<Image>().fillAmount = unitStatusList[i].curHp / unitStatusList[i].fullHp;
                hpText = hpBarList[i].gameObject.transform.Find("HpText").gameObject.GetComponent<Text>();
                hpText.text = (unitStatusList[i].curHp + "/" + unitStatusList[i].fullHp).ToString();
            }
            //�ٵ� isDead���� �ҷ��ͼ� true�̸� SearchUnit() ������ �ʻ� ���� ����Ʈ �ʱ�ȭ�� �ٶ� �׷��µ�
            //1.isDead���� �ҷ��� ������ �ȳ������� 2.Ȥ�� isDead�� �޾Ƽ� SearchUnit() �������� ������ ������ ������ isDead���� ��������� �������   ���� �Ѱ��� ������ ������. ��... ����� ���� �����µ� ū����.
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
