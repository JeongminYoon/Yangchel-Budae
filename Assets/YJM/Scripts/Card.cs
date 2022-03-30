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
    Vector3 cardRtPos;
    RectTransform tr;

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

        tr = GetComponent<RectTransform>();
        cardRtPos = tr.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //UnitStatus 를 찾는 for 문을 돌려서 ex)MeleeUnitStatus = 0 RangeUnitStatus = 1 이 카드의 UnitStatus 번호를 NewCardManager.CardUse()에 넣어주면 되나?  
        //Unit Status에서 번호를 찾아서 그 번호를
        //더 자세히 구현하려면 UnitStatus Scriptable Object를 기획에 맞게 다 만들어야함(지금은 2개밖에 없는거 돌려써서 UnitStatus 명으로 찾으면 백퍼 오류남)

        


    }

    


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {//클릭해서 드래그 시작 했을때
        //카드의 처음위치 cardPos를 정해줌
        cardPos = transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {//드래그 중
        Vector3 currentPos = eventData.position;
        transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {//드롭할때 => 땅이면 소환하고 아니면 시마이.
        RayResult temp = Funcs.RayToWorld();

        if (temp.isHit == false)
        {
            //닿인곳이 땅이 아니면 카드의 처음위치(cardPos)로 카드를 되돌려놓음
            transform.position = cardPos;
        }
        else
        {   //닿인곳이 땅이면 유닛 소환
            
            if (temp.hitObj.tag == "Tower" || temp.hitObj.tag == "Nexus")
            { 
                transform.position = cardPos;
            }
            else 
            {
                UnitFactory.instance.SpawnUnit((Enums.UnitClass)status.unitNum,temp.hitPosition);

                NewCardManager.instance.SpawnCard(this.gameObject, 4);
                NewCardManager.instance.CardUse(this.gameObject);
            }
            

        }

    }

}
