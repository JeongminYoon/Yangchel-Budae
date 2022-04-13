using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CardManager : MonoBehaviour
{
    /// <singletone>
    static public CardManager instance = null;
    /// <singletone>

    public DeckContainer deckContainer;

    public List<UnitStatus> unitStatusList = new List<UnitStatus>();

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
        for (int i = 0; i < unitStatusList.Count; i++) // 유닛리스트만큼 카드를 추가해주고 정렬함
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
                    print("선택카드 : " + myCard[i].GetComponent<CardPrefab>().unitName.text);
                    myCard[i].GetComponent<CardPrefab>().isSelect = false;
                    MyCardToAllCard(i);
                }
            }
        }
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
                    myCard[i] = AllCard[clickedCardNum]; // myCard[?]위치가 null인지 스캔해서 빈 위치에 선택한 AllCard 카드를 넣어줌
                    SortCard(myCard[i], myCardPos, i);
                    swt = true;
                }
            }
        }
        swt = false;
        AllCard.RemoveAt(clickedCardNum); //선택한 AllCard[?]를 지워줌
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


    void SortCard(GameObject gameObject, GameObject deckPos ,int cardNum) //UI라서 RectTransform 값으로 위치를 잡아줌. 한줄당 4카드로 정렬시키기
    {
        RectTransform cardRt = gameObject.GetComponent<RectTransform>();
        cardRt.anchoredPosition = SortCardVec(deckPos, cardNum);
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

    public void GoToInGameScene()
    {
        GameManager.MyHandsList.Clear();
        for (int i = 0; i < myCard.Length; i++)
        {
            if (myCard[i] != null)
            {
                GameManager.MyHandsList.Add(myCard[i].GetComponent<CardPrefab>().status);
            }
        }

        GameManager.instance.SceneChange(Enums.SceneNum.InGame);
    }


    //unitStatusList = allUnitStatus;
    //    myCard = new GameObject[unitStatusList.Count];
    //    for (int i = 0; i<myCard.Length; i++)
    //    {
    //        myCard[i] = Instantiate(cardPrefab, uiCanvas.transform);
    //myCard[i].GetComponent<CardPrefab>().status = ScriptableObject.CreateInstance<UnitStatus>();
    //        myCard[i].GetComponent<CardPrefab>().status.DeepCopy(unitStatusList[i]);
    //        SortCard(myCard[i], i);




    //    myCard[0] = AllCard[0]; // 占쏙옙占쏙옙占쏙옙 占쏙옙체카占쏙옙占?占쏙옙占쏙옙占쏙옙 占싱곤옙 占쏙옙占쏙옙占쏙옙占쏙옙 占쌕꿔서 占쌍깍옙
    //            AllCard.RemoveAt(0);
    //            for (int i = 0; i<AllCard.Count; i++)
    //            {
    //                SortCard(AllCard[i], allCardPos, i); //AllCard.Lengh 占쏙옙 占쏙옙 占쏙옙占쏙옙占쏙옙占쏙옙 占쌍깍옙
    //}
    //SortCard(myCard[0], myCardPos, 0);

}


