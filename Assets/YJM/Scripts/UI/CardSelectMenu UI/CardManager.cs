using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    /// <singletone>
    static public CardManager instance = null;
    /// <singletone>

    public DeckContainer deckContainer;

    public List<UnitStatus> allUnitStatus = new List<UnitStatus>();
    List<UnitStatus> unitStatusList = new List<UnitStatus>();

    List<GameObject> AllCard = new List<GameObject>();
    int AllCardLengh;
    public GameObject[] myCard = new GameObject[8];

    public GameObject cardPrefab;
    public GameObject allCardPos;
    public GameObject myCardPos;

    public GameObject uiCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        unitStatusList = allUnitStatus;
        for (int i = 0; i < unitStatusList.Count; i++) // 처음 시작때 모든카드들을 밑으로 표시
        {
            AllCard.Add(Instantiate(cardPrefab, uiCanvas.transform));
            AllCard[i].GetComponent<CardPrefab>().status = ScriptableObject.CreateInstance<UnitStatus>();
            AllCard[i].GetComponent<CardPrefab>().status.DeepCopy(unitStatusList[i]);
            SortCard(AllCard[i], allCardPos, i);
        }
        AllCardLengh = AllCard.Count;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))   //Save(씬 탈출할때 여기 내용물 실행시킬것)
        {
            deckContainer.deckCardList.Clear();

            for (int i = 0; i < myCard.Length; i++)
            {
                if (myCard[i] != null)
                {
                    UnitStatus stat = myCard[0].GetComponent<CardPrefab>().status; // 왜 null값이 들어가지??? 밑에 print로 찍어보면 분명 stat에 UnitStatus형식의 값이 잘 들어갔는데??
                    deckContainer.deckCardList.Add(stat);
                }
            }
            print(myCard[0].GetComponent<CardPrefab>().status.unitName);
        }

        for (int i = 0; i < AllCard.Count; i++)
        {
            if (AllCard[i] != null)
            {
                if (AllCard[i].GetComponent<CardPrefab>().isSelect == true)
                {
                    AllCard[i].GetComponent<CardPrefab>().isSelect = false;
                    AllCardToMyCard(i);
                }
            }
        }
        for (int i = 0; i < myCard.Length; i++)
        {
            if (myCard[i] != null)
            {
                if (myCard[i].GetComponent<CardPrefab>().isSelect == true)
                {
                    print("덱에서 카드목록으로: " + myCard[i].GetComponent<CardPrefab>().unitName.text);
                    myCard[i].GetComponent<CardPrefab>().isSelect = false;
                    MyCardToAllCard(i);
                }
            }
        }
        //print(AllCard.Count);
    }

    void AllCardToMyCard(int clickedCardNum)
    {
        bool swt = false;
        for (int i = 0; i < myCard.Length; i++)
        {
            if (myCard[i] == null)
            {
                if (swt == false)
                {
                    myCard[i] = AllCard[clickedCardNum]; // 이부분 myCard[?]을 빈 공간을 찾아서 넣어주게 만들기, AllCard[?]는 클릭한 카드를 인식해서 넣어주기
                    SortCard(myCard[i], myCardPos, i);
                    swt = true;
                }
            }
        }
        swt = false;
        AllCard.RemoveAt(clickedCardNum); //AllCard[?]는 클릭한 카드를 인식해서 지워주기
        for (int i = 0; i < AllCard.Count; i++)
        {
            SortCard(AllCard[i], allCardPos, i);
        }
    }

    void MyCardToAllCard(int clickedCardNum)
    {
        AllCard.Add(myCard[clickedCardNum]);
        myCard[clickedCardNum] = null;
        SortCard(AllCard[AllCard.Count - 1], allCardPos, AllCard.Count - 1);
    }


    void SortCard(GameObject gameObject, GameObject deckPos, int cardNum) //카드옵젝, 전체카드or핸드 0/1, 몇번째 위치에 둘지
    {
        RectTransform CardRt = gameObject.GetComponent<RectTransform>();
        CardRt.anchoredPosition = SortCardVec(deckPos, cardNum);
    }

    Vector2 SortCardVec(GameObject Pos, int i)
    {
        int num = i + 1;
        int w = 0;
        int h = Mathf.Abs(i / 4);
        if (num > 4)
        {
            w = num - 4 * h;
        }
        else
        {
            w = num;
        }
        Vector2 result = Pos.GetComponent<RectTransform>().anchoredPosition + new Vector2(w * 260 - 260, 0 - h * 390);
        return result;
    }




    //unitStatusList = allUnitStatus;
    //    myCard = new GameObject[unitStatusList.Count];
    //    for (int i = 0; i<myCard.Length; i++)
    //    {
    //        myCard[i] = Instantiate(cardPrefab, uiCanvas.transform);
    //myCard[i].GetComponent<CardPrefab>().status = ScriptableObject.CreateInstance<UnitStatus>();
    //        myCard[i].GetComponent<CardPrefab>().status.DeepCopy(unitStatusList[i]);
    //        SortCard(myCard[i], i);




    //    myCard[0] = AllCard[0]; // 덱에서 전체카드로 보낼때 이거 변수명만 바꿔서 넣기
    //            AllCard.RemoveAt(0);
    //            for (int i = 0; i<AllCard.Count; i++)
    //            {
    //                SortCard(AllCard[i], allCardPos, i); //AllCard.Lengh 로 맨 마지막에 넣기
    //}
    //SortCard(myCard[0], myCardPos, 0);

}


