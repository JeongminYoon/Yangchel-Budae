using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour
{
    static public HpBarManager instance = null;
    public GameObject hpBarPrefab;
    public GameObject canvas;
    #region debug Value
    //GameObject debugUnit;
    //float debugCurHp = 100f;
    //float debugFulHp = 100f;
    //Vector3 debugPos;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //debugUnit = HpBarInstrate(); // 디버그 소환용도
    }
    float time = 0f;
    void Update()
    {
        //DebugHpBarSet();     // 디버그용도. 실제 사용시 HpBarSetting(CurHp, FulHp, UnitPos, bool isEnemy)를 유닛에 넣어주면됨
        time += Time.deltaTime;
        if (time >= 5f)
        {
            time = -50;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            CameraShake.instance.Shake(3f, 0.1f, 0.05f);
        }
    }

    //void DebugHpBarSet()
    //{
    //    if (debugCurHp > 0f)
    //    {
    //        //debugCurHp -= Time.deltaTime * 10;
    //        debugPos = new Vector3(Random.Range(0, 0), 0f, 0f);
    //        //debugUnit.GetComponent<HpBar>().HpBarSetting(debugCurHp, debugFulHp, debugPos);
    //    }
    //    else
    //    {
    //        Destroy(debugUnit);
    //    }
    //}

    public GameObject HpBarInstrate(Vector3 worldPosition)
    {
        return Instantiate(hpBarPrefab, Camera.main.WorldToScreenPoint(worldPosition + new Vector3(0f, 3.2f, 0f)), Quaternion.identity, canvas.transform);
    }

    #region 구 코드
    ////작동원리:
    ////1.현재 맵상의 유닛/타워를 찾아서(SearchUnit) 리스트로 정리하고(unitList) 그 리스트의 숫자만큼 Hp바가 생성된 리스트도 만들어준다(hpBarList)
    ////2.hpBarList[i]번째를 유닛리스트의 unitList[i] 위치에 뿌려준다
    ////3.hpBarList[i] 번째에 unitList[i]의 체력을 가져와 표시해준다
    ////4.만약 unitList[i].isDead가 활성화 된다면(i 번쨰 유닛 사망) 모든 리스트를(unitList, hpBarList) 초기화 해주고(SearchUnit) 1번으로 돌아간다

    ////문제점:
    ////1.unitList[i].isDead를 값을 받을수 없음. 이미 죽은 오브젝트를 참조하기 떄문. isDead값을 이 스크립트가 받는것보다 유닛이 사라지는게 더 빠른거같음.
    ////ㄴ 다른방법으로는 업데이트마다 unitList숫자를 체크해줘야하는데 클래스에서 유닛리스트를 받아온다는점에서 최적화가 골로가버릴것 같은 느낌이 들어서 못하겠음.
    ////ㄴㄴ 04-11 isDead값을 참조하지많고 직접 유닛 게임오브젝트의 체력값을 받아와서 서순 해결함.
    ////2.유닛이 죽을때마다 모든 리스트를 초기화해준다. 즉 모든 hp바를 제거했다가 다시 만들기 떄문에 깜빡임이 발생함.
    //      //이건 죽은유닛을 인식해서 (unitList의 null값 불러오기) hpBarList만 초기화 하지 말고 갯수맞춰 관리해야하는데 어렵다.. 공부 더해야함

    //static public HpBarManager instance = null;
    //public GameObject canvas;

    //[SerializeField] GameObject hpBarPrefab;

    //List<GameObject> hpBarList = new List<GameObject>();
    //List<GameObject> unitList = new List<GameObject>();

    //Camera cam = null;

    //Text hpText;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //}
    //void Start()
    //{
    //    cam = Camera.main;

    //    SearchUnit(null);
    //}

    //void Update()
    //{
    //    HpBarWork();

    //    if (Input.GetMouseButtonDown(1)) // 리프레쉬 < 이걸 유닛 소환됬을때, 파괴됬을떄 리프레쉬 되게 바꾸면 됨
    //    {
    //        SearchUnit(null);
    //    }
    //}

    //public void SearchUnit(GameObject obj =null)
    //{
    //    ClearhpBarList();
    //    //List<GameObject> fieldUnit = UnitManager.instance.unitList[(int)Enums.Team.ally];
    //    AddUnitList(/*UnitManager.instance.unitList[(int)Enums.Team.ally],*/ (int)Enums.Team.ally);
    //    //List<GameObject> enemyfieldUnit = UnitManager.instance.unitList[(int)Enums.Team.enemy];
    //    AddUnitList(/*enemyfieldUnit, */(int)Enums.Team.enemy);
    //    AddTowerList(unitList);

    //    for (int i = 0; i < unitList.Count; i++)
    //    {
    //        if (unitList[i] != null)
    //        {
    //            GameObject hpBar = Instantiate(hpBarPrefab, unitList[i].transform.position, Quaternion.identity, canvas.transform);
    //            hpBarList.Add(hpBar);
    //        }
    //    }
    //}

    //void ClearhpBarList()
    //{
    //    unitList.Clear();
    //    for (int i = 0; i < hpBarList.Count; i++)
    //    {
    //        Destroy(hpBarList[i]);
    //    }
    //    hpBarList.Clear();
    //}

    //void AddUnitList(/*List<GameObject> unit, */int team)
    //{
    //    List<GameObject>  unit = UnitManager.instance.unitList[team];

    //    for (int i = 0; i < unit.Count; i++)
    //    {
    //        unitList.Add(unit[i]);
    //    }
    //}
    //void AddTowerList(List<GameObject> a)
    //{
    //    a.Add(TowerManager.instance.towerList[(int)Enums.Team.ally,0]);
    //    a.Add(TowerManager.instance.towerList[(int)Enums.Team.ally,1]);
    //    a.Add(TowerManager.instance.towerList[(int)Enums.Team.enemy, 0]);
    //    a.Add(TowerManager.instance.towerList[(int)Enums.Team.enemy, 1]);
    //    a.Add(TowerManager.instance.nexusList[(int)Enums.Team.ally]);
    //    a.Add(TowerManager.instance.nexusList[(int)Enums.Team.enemy]);

    //    a.Remove(null);
    //}
    //void HpBarWork()
    //{
    //    for (int i = 0; i < unitList.Count; i++)
    //    {
    //        //if (unitList[i].GetComponent<Units>().unitStatus.isDead == true) // 작동안함.
    //        //{
    //        //    SearchUnit();
    //        //}
    //        //else 
    //        //{
    //            if (unitList[i] != null)
    //            {
    //                UnitStatus status = unitList[i].GetComponent<Units>().unitStatus;

    //                hpBarList[i].transform.position = cam.WorldToScreenPoint(unitList[i].transform.position + new Vector3(0f, 3.2f, 0f));
    //                hpBarList[i].GetComponent<Image>().fillAmount = status.curHp / status.fullHp;

    //                hpText = hpBarList[i].gameObject.transform.Find("HpText").gameObject.GetComponent<Text>();
    //                hpText.text = (status.curHp + "/" + status.fullHp).ToString();

    //                   if (status.curHp <= 0)
    //                   {
    //                      SearchUnit(null);
    //                   }

    //            }
    //        //}
    //    }
    //}

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
    #endregion
}
