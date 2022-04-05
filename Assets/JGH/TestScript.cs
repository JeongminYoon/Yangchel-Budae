using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;
using Enums;

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
    // [SerializeField]
    // private List<GameObject> listEnemy;
    // [SerializeField]
    // private List<GameObject> listFriendly;
    //  public GameObject[] arrEnemyTower = new GameObject[3];
    // public GameObject[] arrFriendlyTower = new GameObject[3];
    //넥서스는 따로 빼주기 타워 배열 2로 줄이고


    //public List<GameObject> GetEnemyList()
    //{
    //    return listEnemy;
    //    //C#에서 클래스는 call by reference로 됨.
    //    //List도 결국 클래스형이라서 디폴트가 레퍼
    //}


    //int iTemp = 0;

    public GameObject[] unitPrefab = new GameObject[2];
    public GameObject[] unit = new GameObject[2];
    public Vector3[] dir = new Vector3[2];
    public float distance;
    public float[] unitScale = new float[2];
    public float[] unitColScale = new float[2];

    public float colSizeSum;


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


    public void ObjColScale()
    {
        for (int i = 0; i < unitScale.Length; ++i)
        {
            unitScale[i] = unit[i].transform.localScale.x;
            unitColScale[i] = unit[i].GetComponent<CapsuleCollider>().radius;

            colSizeSum += unitScale[i] * unitColScale[i];
        }

        
    }


    public void ObjSetting()
    {
        for (int i = 0; i < unitPrefab.Length; ++i)
        {
            unit[i] = Instantiate(unitPrefab[i], new Vector3(0, 0, i * 30), Quaternion.identity);
        }
    }
    public void ObjMove()
    {
        dir[0] = unit[1].transform.position - unit[0].transform.position;
        dir[1] = unit[0].transform.position - unit[1].transform.position;

        distance = dir[0].magnitude;

        dir[0] = dir[0].normalized;
        dir[1] = dir[1].normalized;

        Debug.Log(distance);

        for (int i = 0; i < unit.Length; ++i)
        {
            if (distance > colSizeSum)
            { 
                unit[i].transform.position += dir[i] * 1f * Time.deltaTime;
            }
        }

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
        //ObjSetting();
        //ObjColScale();

        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition); //unity는 좌측하단이 0,0

        //alpha1 == 49
        for (int i = (int)KeyCode.Alpha1; i < (int)KeyCode.Alpha9; ++i)
        {
            if (Input.GetMouseButtonDown(Defines.left) && Input.GetKey((KeyCode)i))
            {
                Structs.RayResult rayResult = Funcs.RayToWorld();

                if (rayResult.isHit && rayResult.hitObj.tag != "Tower" && rayResult.hitObj.tag != "Nexus")
                {
                    UnitFactory.instance.SpawnUnit((Enums.UnitClass)i - 49, rayResult.hitPosition);
                }

            }
            else if (Input.GetMouseButtonDown(Defines.right) && Input.GetKey((KeyCode)i))
            {
                Structs.RayResult rayResult = Funcs.RayToWorld();

                if (rayResult.isHit && rayResult.hitObj.tag != "Tower" && rayResult.hitObj.tag != "Nexus")
                {
                    UnitFactory.instance.SpawnUnit((Enums.UnitClass)i - 49, rayResult.hitPosition,true);
                }
            }

        }


        //ObjMove();
    }
}
