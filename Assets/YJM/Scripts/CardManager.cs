using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    //1. 진짜 모든 카드 덱 -> 8장
    //2. 손패 ->4장
    //3. 패 묘지 


    int[] allDeck = new int[8];
    int[] myHand = new int[4];
    List<int> grave = new List<int>();

    //1. 준비된 덱 섞기(셔플 알고리즘)
    //2. 섞인 덱중 윗 4장을 myHand로 넘겨주기
    //3. 카드사용하면 그 카드를 grave로 보내기
    //4. 덱에있는 맨 위의 카드를 myHand로 보내기
    //5. 덱에 카드가 없을경우 grave에서 가져오기(선입선출 주의)


    List<int> cardList = new List<int>();

    public static int a;
    public static int b;
    public static int c;
    public static int d;
    // Start is called before the first frame update
    void Start()
    {
        //CardAdd();
    }

    // Update is called once per frame
    void Update()
    {
        //if (cardList .Count <= 0)
       // {
            //CardAdd();
       // }

       PopCard();


        //카드 표시
        //a = cardList[0];
        //b = cardList[1];
        //c = cardList[2];
        //d = cardList[3];
    }

    void CardAdd()
    {
        for (int i = 1; i < 9; i++)
        {
            cardList.Add(i);
            int a = cardList[i];
            print(a);
        }
    }

    
    void PopCard()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 pos = Funcs.RayToWorld().hitPosition;
            UnitFactory.instance.SpawnMeleeUnit(pos);
        }


        //if (Input.GetButtonDown("Fire1"))
        //{
        //    int a = Random.Range(0, 4);
        //    int res = cardList[a];
        //    print(res + "가 뽑힘");
        //    cardList.Remove(res);
        //    cardList.Add(res);
        //}
    }

}
