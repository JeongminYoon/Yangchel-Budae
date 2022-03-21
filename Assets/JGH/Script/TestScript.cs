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

    //0321 수업



    public static TestScript instance = null;

    [SerializeField]
    private List<GameObject> listEnemy;
    //아마 이건 게임 매니저에 놔둘 듯 나중에는


    public List<GameObject> GetEnemyList()
    {
        return listEnemy;
        //C#에서 클래스는 call by reference로 됨.
        //List도 결국 클래스형이라서 디폴트가 레퍼
    }
    

    int iTemp = 0;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnitFactory.instance.SpawnMeleeUnit(new Vector3(0f, 1f, -5.5f));
        }

        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    UnitFactory.instance.SpawnRangeUnit(new Vector3(0f, 1f, 0f));
        //}


        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            listEnemy.Add(UnitFactory.instance.SpawnEnemy(new Vector3(0f, 1f, 6.04f)));
            //실제로도 적이 생성될때만 추가하기.
        }
    }
}
