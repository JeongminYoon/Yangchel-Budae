using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardManager : MonoBehaviour
{
    //1. 진짜 모든 카드 덱 -> 8장
    //2. 손패 ->4장
    public GameObject uiCanvas;
    public GameObject cardPrefab;
    public GameObject[] Pos = new GameObject[4];
    RectTransform tr;

    int[] deck;
    int[] myHand = new int[4];
    List<int> grave = new List<int>();
    List<int> deckList = new List<int>();
    int rnd;

    public List<UnitStatus> asdf;

    void Start()
    {

        //처음 덱 섞기
        deck = new int[asdf.Count];
        CardAdd();
        ShuffleArray(deck);
        for (int i = 0; i < deck.Length; i++)
        {
            deckList.Add(deck[i]);
        }
        //까지 start up

        //처음 for문 돌려서 list 덱의 값을 hand안에 다 넣어준다
        for (int i = 0; i < 4; i++)
        {
            myHand[i] = deckList[i];
            print(myHand[i]);
            SpawnCard(myHand[i]); // 손패에 들어온 데이터4개
        }
        //즉 이때 손패 카드4개가 뿌려진거임

    }

    // Update is called once per frame
    void Update()
    {

        // 99을 찾는 변수를 for문으로 돌려서(사용한 카드 자리 찾기) 사용한 카드의 자리에 deckList / grave 의 선입을 넣어줌
        for (int i = 0; i < 4; i++)
        {
            if (myHand[i] == 99)
            {
                if (grave.Count < 4)
                {
                    myHand[i] = deckList[0];
                }
                else
                {
                    myHand[i] = grave[i];
                }
            }
        }

    }
    void CardAdd()
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = i;
        }
    }


    void SpawnCard(int i)
    {
        GameObject tempCard = Instantiate(cardPrefab, uiCanvas.transform);
        GameObject RectPos = Pos[i];
        tr = RectPos.GetComponent<RectTransform>();
        tempCard.transform.position = tr.position;
        Card card = tempCard.GetComponent<Card>();
        card.status = asdf[i];
    }

    private T[] ShuffleArray<T>(T[] array)
    {
        int random1, random2;
        T temp;

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

}




