using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public UnitStatus status;
    public Text unitName;
    public Text unitCost;

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
        
    }
}
