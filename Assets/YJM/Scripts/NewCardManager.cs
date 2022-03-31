﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardManager : MonoBehaviour
{
    /// <singletone>
    static public NewCardManager instance = null;
    /// <singletone>

    //1. 진짜 모든 카드 덱 -> 8장
    //2. 손패 ->4장
    public GameObject uiCanvas;
    public GameObject cardPrefab;
    public GameObject[] Pos = new GameObject[5];
    public Queue<GameObject> deckqueue;

    GameObject[] deck; //로비에서 넘어와서 셔플 할 용도로 사용할 배열
    public GameObject[] myHand = new GameObject[4];
    List<GameObject> grave = new List<GameObject>();

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



        for (int i = 0; i < 4; i++)
        {
            myHand[i] = deck[i];
            //print(i + " : " + myHand[i].GetComponent<Card>().status.unitName);
            SpawnCard(myHand[i],i); // 손패에 들어온 데이터4개
        }

        for (int i = 0; i < 4; i++)
        {
            grave.Add(deck[i+4]);
        }
        //즉 이때 손패 카드4개가 뿌려진거임

        //HandCheck();
        //DeckCheck();
        //GraveCheck();
    }

    void Update()
    {
        
    }

    void CardAdd()
    {//여기서 카드 생성.
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = Instantiate(cardPrefab,uiCanvas.transform);
            deck[i].transform.position = Pos[4].transform.position;
            deck[i].GetComponent<Card>().status = ScriptableObject.CreateInstance<UnitStatus>();
            deck[i].GetComponent<Card>().status.DeepCopy(unitDataList[i]);
        }
    }



    public void SpawnCard(GameObject _card, int index)
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


    public void CardUse(GameObject card)
    {
        for (int i = 0;i < 4; ++i)
        {
            if (myHand[i] == card)
            {
                grave.Add(myHand[i]);
                myHand[i] = null;
            }
        }
        CardLoop();
        HandCheck();
        DeckCheck();
        GraveCheck();
    }

    void CardLoop()
    {
        for (int i = 0; i < 4; i++)
        {
            if (myHand[i] == null)
            {
                SpawnCard(grave[0], i);
                    myHand[i] = grave[0];
                    grave.RemoveAt(0);
                    
            }
        }
    }

    void HandCheck()
    {
        for (int i = 0; i < myHand.Length; i++)
        {
            print("myHand : " + myHand[i].GetComponent<Card>().status.unitName);
        }
    }

    void DeckCheck()
    {
        for (int i = 0; i < deck.Length; i++)
        {
            print("deck : " + deck[i].GetComponent<Card>().status.unitName);
        }
    }

    void GraveCheck()
    {
        for (int i = 0; i < grave.Count; i++)
        {
            print("grave : " + grave[i].GetComponent<Card>().status.unitName);
        }
    }

}