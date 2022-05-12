using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CardManager : MonoBehaviour
{
    #region singletone
    /// <singletone>    
    static public CardManager instance = null;
    /// <singletone>

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }
    #endregion


    public DeckContainer deckContainer;

    public List<UnitStatus> unitStatusList = new List<UnitStatus>();

    List<GameObject> AllCard = new List<GameObject>();
    public GameObject[] myCard = new GameObject[6];

    public GameObject cardPrefab;
    public GameObject allCardPos;
    public GameObject myCardPos;

    public GameObject uiCanvas;
    public GameObject costText;
    public GameObject infoText;

    void Start()
    {
        for (int i = 0; i < unitStatusList.Count; i++) // 유닛리스트만큼 카드를 추가해주고 정렬함
        {
            AllCard.Add(Instantiate(cardPrefab, uiCanvas.transform));
            AllCard[i].GetComponent<CardPrefab>().status = ScriptableObject.CreateInstance<UnitStatus>();
            AllCard[i].GetComponent<CardPrefab>().status.DeepCopy(unitStatusList[i]);
            SortCard(AllCard[i], allCardPos, i, 4);
        }
        SetCostText();
        SetInfoMessage();
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
                    if (AllCard.Count > (unitStatusList.Count - myCard.Length))
                    { 
                        AllCardToMyCard(i);
                        SetCostText();
                        SetInfoMessage(); 
                    }
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
                    SetCostText();
                    SetInfoMessage();
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
                    SortCard(myCard[i], myCardPos, i, 3);
                    swt = true;
                }
            }
        }
        swt = false;
        AllCard.RemoveAt(clickedCardNum); //선택한 AllCard[?]를 지워줌
        for (int i = 0; i < AllCard.Count; i++)
        {
            SortCard(AllCard[i], allCardPos, i, 4);
        }
    }

    void MyCardToAllCard(int clickedCardNum)
    {
        AllCard.Add(myCard[clickedCardNum]);
        myCard[clickedCardNum] = null;
        SortCard(AllCard[AllCard.Count - 1], allCardPos, AllCard.Count - 1, 4);
    }


    void SortCard(GameObject gameObject, GameObject deckPos ,int cardNum, int cardW) //UI라서 RectTransform 값으로 위치를 잡아줌. 한줄당 4카드로 정렬시키기
    {
        RectTransform cardRt = gameObject.GetComponent<RectTransform>();
        cardRt.anchoredPosition = SortCardVec(deckPos, cardNum, cardW);
    }

    Vector2 SortCardVec(GameObject Pos, int cardNum, int cardW)
    {
        int num = cardNum + 1;
        int w = 0;
        int cardWsize;
        if (cardW == 4)
        {
            cardWsize = 260;
        }
        else
        {
            cardWsize = 350;
        }

        int h = Mathf.Abs(cardNum / cardW);
        if (num > cardW)
        {
            w = num - cardW * h;
        }
        else
        {
            w = num;
        }
        Vector2 result = Pos.GetComponent<RectTransform>().anchoredPosition + new Vector2(w * cardWsize - cardWsize, 0 - h * 330);
        return result;
    }

    public void GoToInGameScene()
    {
        if (isReady == true)
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
    }

    void SetCostText()
    {
        int totalCost = 0;
        for (int i = 0; i < myCard.Length; i++)
        {
            if (myCard[i] != null)
            {
                totalCost += myCard[i].GetComponent<CardPrefab>().status.cost;
                print(myCard[i].GetComponent<CardPrefab>().status.cost);
            }
        }
        Text text = costText.GetComponent<Text>();
        text.text = ":" + totalCost.ToString();
    }

    public bool isReady = false;
    void SetInfoMessage()
    {
        int cardNum = 0;
        {
            for (int i = 0; i < myCard.Length; i++)
            {
                if (myCard[i] != null)
                {
                    cardNum += 1;
                }
            }
        }
        Text text = infoText.GetComponent<Text>();
        if (cardNum < 4)
        {
            text.text = "<color=#930500>" + "최소 " + (4 - cardNum).ToString() + "명이 더 필요합니다" + "</color>";
            NextButton.instance.ChangeButtonColor(0);
            isReady = false;
        }
        else
        {
            text.text = "<color=#009304>" + "출동준비 완료!" + "</color>";
            NextButton.instance.ChangeButtonColor(1);
            isReady = true;
        }    
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


