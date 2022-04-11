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

    public List<UnitStatus> allUnitStatus = new List<UnitStatus>();
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
        unitStatusList = allUnitStatus;
        for (int i = 0; i < unitStatusList.Count; i++) // ó�� ���۶� ���ī����� ������ ǥ��
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
        if (Input.GetMouseButtonDown(1))   //Save(�� Ż���Ҷ� ���� ���빰 �����ų��?
        {
                GameManager.MyHandsList.Clear();
            for (int i = 0; i < myCard.Length; i++)
            {
                if (myCard[i] != null)
                {
                    GameManager.MyHandsList.Add(myCard[i].GetComponent<CardPrefab>().status);
                }
            }
            SceneManager.LoadScene(0);
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
                    print("������ ī��������: " + myCard[i].GetComponent<CardPrefab>().unitName.text);
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
                    myCard[i] = AllCard[clickedCardNum]; // �̺κ� myCard[?]�� �� ������ ã�Ƽ� �־��ְ� �����? AllCard[?]�� Ŭ���� ī�带 �ν��ؼ� �־��ֱ�
                    SortCard(myCard[i], myCardPos, i);
                    swt = true;
                }
            }
        }
        swt = false;
        AllCard.RemoveAt(clickedCardNum); //AllCard[?]�� Ŭ���� ī�带 �ν��ؼ� �����ֱ�
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


    void SortCard(GameObject gameObject, GameObject deckPos ,int cardNum) //ī�����? ��üī��or�ڵ�, ����?��ġ�� ����
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




    //    myCard[0] = AllCard[0]; // ������ ��üī���?������ �̰� �������� �ٲ㼭 �ֱ�
    //            AllCard.RemoveAt(0);
    //            for (int i = 0; i<AllCard.Count; i++)
    //            {
    //                SortCard(AllCard[i], allCardPos, i); //AllCard.Lengh �� �� �������� �ֱ�
    //}
    //SortCard(myCard[0], myCardPos, 0);

}


