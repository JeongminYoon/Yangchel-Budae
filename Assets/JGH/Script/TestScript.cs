using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    //0321 복습
    //V => 속도 : 방향과 크기가 있는 벡터
    //S => 속력 : 크기만 있는 스칼라
    //속도 = 방향 * 시간 * 속력
    //Vector3.right -> 사전에 정의되어 있는 (1f, 0f, 0f) 절대값고
    //tr.right -> 현재 Object의 Right방향.(회전값에 떠러 더룸)


    public static TestScript instance = null;

    //[SerializeField]
    //아마 얘들은 오브젝트 매니저에 놔둘 듯 나중에는
    [SerializeField]
    private List<GameObject> listEnemy;
    [SerializeField]
    private List<GameObject> listFriendly;
    public GameObject[] arrEnemyTower = new GameObject[3];
    public GameObject[] arrFriendlyTower = new GameObject[3];
    //넥서스는 따로 빼주기 타워 배열 2로 줄이고


    public List<GameObject> GetEnemyList()
    {
        return listEnemy;
        //C#에서 클래스는 call by reference로 됨.
        //List도 결국 클래스형이라서 디폴트가 레퍼
    }


    //int iTemp = 0;

    public Vector3 ScreenToWorld()
    {//작동안됨 ㄴㄴㄴㄴ
        Vector2 mousePos = new Vector2();
        Event currentEvent = Event.current;
        Vector3 worldPos = new Vector3();

        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = Camera.main.pixelHeight - currentEvent.mousePosition.y;

        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        return worldPos;
    }

    public Vector3 RayToWorld(int mouseButton)
    {
        Vector3 worldPos = new Vector3();

        if (Input.GetMouseButtonDown(mouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit castHit;
            if (Physics.Raycast(ray, out castHit))
            {
                worldPos = castHit.point;
                worldPos.y = 1f;
            }
        }

        return worldPos;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()

    {
        listEnemy.Clear();
        //iTemp = Units.instance.GetSetInt;
        //Debug.Log(iTemp);

        //Units.instance.GetSetInt = 1;
        //Debug.Log(Units.instance.GetSetInt);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition); //unity는 좌측하단이 0,0

        
        if (Input.GetMouseButtonDown(ConstVariables.leftMouse) && Input.GetKey(KeyCode.None) ) //좌클릭
        {
            //listFriendly.Add(UnitFactory.instance.SpawnMeleeUnit(new Vector3(2f, 1f, -5.5f)));
            listFriendly.Add(UnitFactory.instance.SpawnMeleeUnit(RayToWorld(ConstVariables.leftMouse)));
        }

        if (Input.GetMouseButtonDown(ConstVariables.leftMouse) && Input.GetKey(KeyCode.LeftControl))
        {
            listFriendly.Add(UnitFactory.instance.SpawnRangeUnit(RayToWorld(ConstVariables.leftMouse)));
        }


        if (Input.GetMouseButtonDown(ConstVariables.rightMouse)) //우클릭
        {
            //listEnemy.Add(UnitFactory.instance.SpawnEnemy(new Vector3(-2f, 1f, 6.04f)));
            listEnemy.Add(UnitFactory.instance.SpawnEnemy(RayToWorld(ConstVariables.rightMouse)));
            //실제로도 적이 생성될때만 추가하기.
        }
    }
}
