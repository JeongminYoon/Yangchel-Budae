using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardManager : MonoBehaviour
{
    //1. 진짜 모든 카드 덱 -> 8장
    //2. 손패 ->4장
    //3. 패 묘지 
    public Canvas uiCanvas;
    public GameObject cardPrefab;

    int[] deck = new int[8];
    int[] myHand = new int[4];
    //List<int> grave = new List<int>();

    public List<UnitStatus> asdf;

    //1. 준비된 덱 섞기(셔플 알고리즘)
    //2. 섞인 덱중 윗 4장을 myHand로 넘겨주기
    //3. 카드사용하면 그 카드를 grave로 보내기
    //4. 덱에있는 맨 위의 카드를 myHand로 보내기
    //5. 덱에 카드가 없을경우 grave에서 가져오기(선입선출 주의)

    //public static int a ,b ,c ,d;

    void Start()
    {
        #region Test
        //for (int i = 0; i < 8; i++)
        //{
        //    allDeck[i] = i+1;
        //}
        //allDeck = ShuffleArray(allDeck);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject tempCard =  Instantiate(cardPrefab,uiCanvas.transform);
            Card card = tempCard.GetComponent<Card>();





            card.status = asdf[0];

        }
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


