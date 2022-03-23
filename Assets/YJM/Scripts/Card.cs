using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public UnitStatus status;
    public Text unitName;
    public Text unitCost;
    Vector3 cardPos;
    float currentPos = 0f;


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

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position.y - cardPos.y;
        
        print(currentPos);
        
    }


    Vector3 RayToWorldTest()
    {
        Vector3 worldPos = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit castHit;
        if (Physics.Raycast(ray, out castHit))
        {
            worldPos = castHit.point;
            worldPos.y = 1f;
        }
        return worldPos;
    }


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        cardPos = transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 currentPos = eventData.position;
        transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos = RayToWorldTest();
        if (pos == new Vector3(0f, 0f, 0f))
        {
            transform.position = cardPos;
        }
        else
        {
            UnitFactory.instance.SpawnMeleeUnit(pos);
            Destroy(gameObject);
        }

    }

}
