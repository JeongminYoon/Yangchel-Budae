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
        if (Input.GetMouseButtonDown(1)) // 리프레쉬 < 이걸 유닛 소환됬을때, 파괴됬을떄 리프레쉬 되게 바꾸면 됨
        {//소환됬을때에는 Card.cs에 트리거, 파괴됬을때에는 Hp바 구현하면서 유닛에게서 상속받은 hp값이 0이 되면 호출시키게 하면 되겠네
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
        //a.Add(TowerManager.instance.nexusList[(int)Enums.Team.ally]);  //이거는 추가하면 에러뜸. 왜지????
        //a.Add(TowerManager.instance.nexusList[(int)Enums.Team.enemy]);
    }
    void HpBarWork()
    {
        for (int i = 0; i < unitList.Count; i++) // 오류: HpBar가 참조하고있던 유닛이 뒤지면 오류남. 
        {
            if (unitStatusList[i].isDead != true) // 일단 예외문줘서 크리티컬한 오류뜨는건 막음
            {
                hpBarList[i].transform.position = cam.WorldToScreenPoint(unitList[i].transform.position + new Vector3(0f, 1.2f, 0f));
                hpBarList[i].GetComponent<Image>().fillAmount = unitStatusList[i].curHp / unitStatusList[i].fullHp;
                hpText = hpBarList[i].gameObject.transform.Find("HpText").gameObject.GetComponent<Text>();
                hpText.text = (unitStatusList[i].curHp + "/" + unitStatusList[i].fullHp).ToString();
            }
            //근데 isDead값을 불러와서 true이면 SearchUnit() 돌려서 맵상 유닛 리스트 초기화해 줄라 그랬는데
            //1.isDead값을 불러올 유닛이 안남아있음 2.혹은 isDead값 받아서 SearchUnit() 돌렸을때 서순이 꼬여서 유닛이 isDead값만 실행해줬고 살아있음   둘중 한가지 이유로 에러뜸. 음... 디버그 할줄 몰르는데 큰났네.
        }
    }


    //유닛이 소환되서 스테이터스 스크립터블 오브젝트값을 받기전에 SearchUnit이 되어버리는 문제가 있었음, 스테이터스 스크립터블 오브젝트값을 Awake()에서 받아오는걸로 바꿔서 해결했지만
    //추후 문제가 생긴다면 유닛리스트 스테이터스 값을 이 객체가 불러서 가지고 있는게 아니라 유닛쪽에 스크립트를 넣어서 체력이 바뀔때라던가, 업데이트에서 계속 돌리는걸로 바꿔야함








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
