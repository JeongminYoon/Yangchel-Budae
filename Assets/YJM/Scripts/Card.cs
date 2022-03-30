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
        //UnitStatus �� ã�� for ���� ������ ex)MeleeUnitStatus = 0 RangeUnitStatus = 1 �� ī���� UnitStatus ��ȣ�� NewCardManager.CardUse()�� �־��ָ� �ǳ�?  
        //Unit Status���� ��ȣ�� ã�Ƽ� �� ��ȣ��
        //�� �ڼ��� �����Ϸ��� UnitStatus Scriptable Object�� ��ȹ�� �°� �� ��������(������ 2���ۿ� ���°� �����Ἥ UnitStatus ������ ã���� ���� ������)

        


    }

    


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {//Ŭ���ؼ� �巡�� ���� ������
        //ī���� ó����ġ cardPos�� ������
        cardPos = transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {//�巡�� ��
        Vector3 currentPos = eventData.position;
        transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {//����Ҷ� => ���̸� ��ȯ�ϰ� �ƴϸ� �ø���.
        RayResult temp = Funcs.RayToWorld();

        if (temp.isHit == false)
        {
            //���ΰ��� ���� �ƴϸ� ī���� ó����ġ(cardPos)�� ī�带 �ǵ�������
            transform.position = cardPos;
        }
        else
        {   //���ΰ��� ���̸� ���� ��ȯ
            
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
