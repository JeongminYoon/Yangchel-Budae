using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardManager : MonoBehaviour
{
    /// <singletone>
    static public NewCardManager instance = null;
    /// <singletone>


    //할것
    //1. deckList, myHand 스크립터블 오브젝트 만들기
    //2. 

    //1. 진짜 모든 카드 덱 -> 8장
    //2. 손패 ->4장
    public GameObject uiCanvas;
    public GameObject cardPrefab;
    public GameObject[] Pos = new GameObject[4];
    public Queue<GameObject> deckqueue;
    //RectTransform tr;

    GameObject[] deck; //로비에서 넘어와서 셔플 할 용도로 사용할 배열
    public GameObject[] myHand = new GameObject[4];
    List<GameObject> grave = new List<GameObject>();
    ///int rnd;

    public List<UnitStatus> unitDataList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

        //처음 덱 섞기
        deck = new GameObject[unitDataList.Count];
        CardAdd();
        ShuffleArray(deck);

        //for (int i = 0; i < deck.Length; ++i)
        //{
        //    print(i + " : " + deck[i].GetComponent<Card>().status.unitName);
           
        //}
        //까지 start up

        //처음 for문 돌려서 list 덱의 값을 hand안에 다 넣어준다
        for (int i = 0; i < 4; i++)
        {
            myHand[i] = deck[i];
            //print(i + " : " + myHand[i].GetComponent<Card>().status.unitName);
            SpawnCard(myHand[i],i); // 손패에 들어온 데이터4개
        }

        

        //즉 이때 손패 카드4개가 뿌려진거임

    }

    // Update is called once per frame
    void Update()
    {
        CardLoop();
    }

    void CardAdd()
    {//여기서 카드 생성.
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = Instantiate(cardPrefab,uiCanvas.transform);
            deck[i].GetComponent<Card>().status = ScriptableObject.CreateInstance<UnitStatus>();
            deck[i].GetComponent<Card>().status.DeepCopy(unitDataList[i]);
        }
    }



    void SpawnCard(GameObject _card, int index)
    {
        GameObject RectPos = Pos[index];
        RectTransform tr = RectPos.GetComponent<RectTransform>();
        _card.transform.position = tr.position;
        //card.status = UnitData[i];
    }

    private GameObject[] ShuffleArray<GameObject>(GameObject[] array)
    {
        int random1, random2;
        GameObject temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }


    public void CardUse(int card)
    {
        //사용한 Card 오브젝트의  
    }

    void CardLoop()
    {
        for (int i = 0; i < 4; i++)
        {
            if (myHand[i] == null)
            {
                if (grave.Count < 4)
                {
                    myHand[i] = deck[0];
                }
                else
                {
                    myHand[i] = grave[i];
                }
            }
        }
    }

}