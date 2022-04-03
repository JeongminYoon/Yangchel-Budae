using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        HpBarWork();    
        if (Input.GetMouseButtonDown(1)) // 리프레쉬 < 이걸 유닛 소환됬을때, 파괴됬을떄 리프레쉬 되게 바꾸면 됨
        {//소환됬을때에는 Card.cs에 트리거, 파괴됬을때에는 Hp바 구현하면서 유닛에게서 상속받은 hp값이 0이 되면 호출시키게 하면 되겠네
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
            unitStatusList.Add(unitList[i].GetComponent<Units>().unitStatus); //unitStatusList에 유닛리스트의 스테이터스값을 넣고싶음
        }
    }

    void HpBarWork()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            hpBarList[i].transform.position = cam.WorldToScreenPoint(unitList[i].transform.position + new Vector3(0f, 1.2f, 0f));
            hpBarList[i].GetComponent<Image>().fillAmount = unitStatusList[i].hp / 100f; // 문제가있음. unitStatusList[i].hp 값을 0~1로 표현해야 하는데 어떻게하지? 스크립터블 오브젝트에서 불러와야하나 아님 최초hp값을 어디 따로 저장해야하나
            if (unitStatusList[i].hp <= 0f)
            {
                SearchUnit();
            }

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
