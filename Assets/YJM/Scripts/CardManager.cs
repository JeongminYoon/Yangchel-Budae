using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    List<int> cardList = new List<int>();
    public static int a;
    public static int b;
    public static int c;
    public static int d;
    // Start is called before the first frame update
    void Start()
    {
        CardAdd();
    }

    // Update is called once per frame
    void Update()
    {
        if (cardList .Count <= 0)
        {
            CardAdd();
        }

        PopCard();


        //ī�� ǥ��
        a = cardList[0];
        b = cardList[1];
        c = cardList[2];
        d = cardList[3];
    }

    void CardAdd()
    {
        for (int i = 0; i < 8; i++)
        {
            cardList.Add(i);
            //int a = cardList[i];
            //print(a);
        }
    }

    

    void PopCard()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int a = Random.Range(0, 4);
            print(cardList[a]+ "�� ����");
            cardList.RemoveAt(a);
            cardList.Add(a);
        }
    }

}
