using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour
{
    //�۵�����:
    //1.���� �ʻ��� ����/Ÿ���� ã�Ƽ�(SearchUnit) ����Ʈ�� �����ϰ�(unitList) �� ����Ʈ�� ���ڸ�ŭ Hp�ٰ� ������ ����Ʈ�� ������ش�(hpBarList)
    //2.hpBarList[i] ��°�� unitList[i]�� ü���� ������ ǥ�����ش�
    //3.���� unitList[i].isDead�� Ȱ��ȭ �ȴٸ�(i ���� ���� ���) ��� ����Ʈ��(unitList, hpBarList) �ʱ�ȭ ���ְ�(SearchUnit) 1������ ���ư���

    //������:
    //1.unitList[i].isDead�� ���� ������ ����. �̹� ���� ������Ʈ�� �����ϱ� ����. isDead���� �� ��ũ��Ʈ�� �޴°ͺ��� ������ ������°� �� �����Ű���.
    //2.������ ���������� ��� ����Ʈ�� �ʱ�ȭ���ش�. �� ��� hp�ٸ� �����ߴٰ� �ٽ� ����� ������ �������� �߻���. �̰� ���������� �ν��ؼ� (unitList�� null�� �ҷ�����) hpBarList�� �ʱ�ȭ ���� ���� �������� �����ؾ��ϴµ� ��ƴ�.. ���� ���ؾ���
    //���� ���۷��� ���� ����ϱ� �����. ���Z ���� �����ϳ�

    static public HpBarManager instance = null;

    [SerializeField] GameObject hpBarPrefab;

    List<GameObject> hpBarList = new List<GameObject>();
    List<GameObject> unitList = new List<GameObject>();

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

    void Update()
    {
        HpBarWork();
        if (Input.GetMouseButtonDown(1)) // �������� < �̰� ���� ��ȯ������, �ı������� �������� �ǰ� �ٲٸ� ��
        {
            SearchUnit();
        }
    }

    public void SearchUnit()
    {
        ClearhpBarList();
        List<GameObject> fieldUnit = UnitManager.instance.unitList[(int)Enums.Team.ally];
        AddUnitList(fieldUnit, (int)Enums.Team.ally);
        List<GameObject> enemyfieldUnit = UnitManager.instance.unitList[(int)Enums.Team.enemy];
        AddUnitList(enemyfieldUnit, (int)Enums.Team.enemy);
        AddTowerList(unitList);

        for (int i = 0; i < unitList.Count; i++)
        {
            if (unitList[i] != null)
            {
                GameObject hpBar = Instantiate(hpBarPrefab, unitList[i].transform.position, Quaternion.identity, transform);
                hpBarList.Add(hpBar);
            }
        }
    }

    void ClearhpBarList()
    {
        unitList.Clear();
        for (int i = 0; i < hpBarList.Count; i++)
        {
            Destroy(hpBarList[i]);
        }
        hpBarList.Clear();
    }

    void AddUnitList(List<GameObject> unit, int team)
    {
        unit = UnitManager.instance.unitList[team];
        for (int i = 0; i < unit.Count; i++)
        {
            unitList.Add(unit[i]);
        }
    }
    void AddTowerList(List<GameObject> a)
    {
        a.Add(TowerManager.instance.towerList[(int)Enums.Team.ally,0]);
        a.Add(TowerManager.instance.towerList[(int)Enums.Team.ally,1]);
        a.Add(TowerManager.instance.towerList[(int)Enums.Team.enemy, 0]);
        a.Add(TowerManager.instance.towerList[(int)Enums.Team.enemy, 1]);
        a.Add(TowerManager.instance.nexusList[(int)Enums.Team.ally]);
        a.Add(TowerManager.instance.nexusList[(int)Enums.Team.enemy]);
    }
    void HpBarWork()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            //if (unitList[i].GetComponent<Units>().unitStatus.isDead == true) // �۵�����.
            //{
            //    SearchUnit();
            //}
            //else 
            //{
                if (unitList[i] != null)
                {
                    UnitStatus status = unitList[i].GetComponent<Units>().unitStatus;
                    status = unitList[i].GetComponent<Units>().unitStatus;
                        hpBarList[i].transform.position = cam.WorldToScreenPoint(unitList[i].transform.position + new Vector3(0f, 1.2f, 0f));
                        hpBarList[i].GetComponent<Image>().fillAmount = status.curHp / status.fullHp;
                        hpText = hpBarList[i].gameObject.transform.Find("HpText").gameObject.GetComponent<Text>();
                        hpText.text = (status.curHp + "/" + status.fullHp).ToString();
                }
            //}
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
