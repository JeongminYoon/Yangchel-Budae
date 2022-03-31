using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Structs;

public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public UnitStatus status;
    public Text unitName;
    public Text unitCost;
    public int cardDeskPos;
    Vector3 cardPos;

    public int value;

    public bool isenabled;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        unitName = this.gameObject.transform.Find("Name").gameObject.GetComponent<Text>();
        unitCost = this.gameObject.transform.Find("Cost").gameObject.GetComponent<Text>();


        unitName.text = status.unitName;
        unitCost.text = (status.cost).ToString();
    }

    RectTransform rt1;
    RectTransform rt2;
    Vector3 Rt1;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {//드래그 시작 했을때
        //카드의 처음위치 cardPos를 정해줌
        cardPos = transform.position;

        //카드 애니메이션 변수 선언
        rt1 = GetComponent<RectTransform>();
        rt2 = GetComponent<RectTransform>();
        Rt1 = rt1.anchoredPosition;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {//드래그 중
        Vector3 currentPos = eventData.position;
        transform.position = currentPos;
        //카드 애니메이션 재생
        CardAnim();
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {//드롭할때
        RayResult temp = Funcs.RayToWorld();

        if (temp.isHit == false)
        {
            //닿인곳이 땅이 아니면 카드의 처음위치(cardPos)로 카드를 되돌려놓음
            transform.position = cardPos;
        }
        else
        {   //닿인곳이 땅이면 유닛 소환
            
            if (temp.hitObj.tag == "Tower" || temp.hitObj.tag == "Nexus" || float.Parse(unitCost.text) > CostManager.instance.currentCost)
            { //닿인곳이 타워,넥서스면 소환 취소
                transform.position = cardPos;
            }
            else 
            {
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)status.unitNum,temp.hitPosition);
                NewCardManager.instance.SpawnCard(this.gameObject, 4);
                NewCardManager.instance.CardUse(this.gameObject);
                CostManager.instance.currentCost -= float.Parse(unitCost.text);
            }
        }
        //카드 애니메이션 초기화
        transform.localScale = new Vector3(1, 1, 1);
    }

    void CardAnim()
    {
        float h = (rt2.anchoredPosition.y - Rt1.y) / 219;
        if (h >= 1)
        {
            h = 1;
        }
        if (h <= 0)
        {
            h = 0;
        }
        transform.localScale = new Vector3(1 - h, 1 - h, 1);
    }
}
