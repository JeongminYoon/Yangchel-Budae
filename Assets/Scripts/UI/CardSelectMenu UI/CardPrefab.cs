using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardPrefab : MonoBehaviour, IPointerClickHandler
{
    public UnitStatus status;
    public Text unitName;
    public Text unitCost;
    public int cardDeskPos;
    public bool isSelect = false;

    public GameObject cardGO;

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


    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("클릭된카드: " + unitName.text);
            isSelect = true;
        }
    }
}
