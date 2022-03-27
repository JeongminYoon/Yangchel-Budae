using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardManager : MonoBehaviour
{
    //1. 진짜 모든 카드 덱 -> 8장
    //2. 손패 ->4장
    public GameObject uiCanvas;
    public GameObject cardPrefab;
    public GameObject Pos1, Pos2, Pos3, Pos4;
    RectTransform tr;

    int[] deck;
    List<int> deckList;

    public List<UnitStatus> asdf;

    void Start()
    {

        //처음 덱 섞기
        deck = new int[asdf.Count];
        CardAdd();
        ShuffleArray(deck);

        deckList = new List<int>();
        for (int i = 0; i < deck.Length; i++)
        {
            deckList.Add(deck[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            print(deckList[0]);
            SpawnCard();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
        }
    }

    void CardAdd()
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = i;
        }
    }


    void SpawnCard()
    {
        int i = 0;

        GameObject tempCard = Instantiate(cardPrefab, uiCanvas.transform);
        GameObject RectPos = Pos1;
        tr = RectPos.GetComponent<RectTransform>();
        tempCard.transform.position = tr.position;
        Card card = tempCard.GetComponent<Card>();
        card.status = asdf[deckList[0]];

        deckList.Insert(deck.Length, deckList[0]);
        deckList.RemoveAt(deckList[0]);
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


