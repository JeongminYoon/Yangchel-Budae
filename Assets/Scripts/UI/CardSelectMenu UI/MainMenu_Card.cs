using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenu_Card : MonoBehaviour, IPointerClickHandler
{
    public UnitStatus status;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitCost;
    public TextMeshProUGUI unitHp;
    public Image charaImage;
    public Image frameImage;
    public Image glowImage;
    public Image typeImage;
    public int cardDeskPos;
    public bool isSelect = false;
    public Sprite[] UnitSprites = new Sprite[6];
    public Sprite[] unitTypeSprites = new Sprite[4]; //melee, range, utility, skill
    public Sprite[] cardFrames = new Sprite[2];

    public GameObject cardGO;
    // Start is called before the first frame update
    void Start()
    {
        //unitName = this.gameObject.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>();
        //unitCost = this.gameObject.transform.Find("Cost").gameObject.GetComponent<TextMeshProUGUI>();

        if (status.unitName.Contains("Skill"))
        {
            frameImage.sprite = cardFrames[1];
            glowImage.color = new Color(0.73f, 0.26f, 0.77f);
        }
        else
        {
            frameImage.sprite = cardFrames[0];
            glowImage.color = new Color(0f, 0.69f, 0.44f);
        }

        if (status.unitName.Contains("Melee"))
        {
            typeImage.sprite = unitTypeSprites[0];
        }
        else if (status.unitName.Contains("Range"))
        {
            typeImage.sprite = unitTypeSprites[1];
        }
        else if (status.unitName.Contains("Skill"))
        {
            typeImage.sprite = unitTypeSprites[3];
        }
        else
        {
            typeImage.sprite = unitTypeSprites[2];
        }

        unitName.text = status.unitName;
        unitCost.text = (status.cost).ToString();
        unitHp.text = status.fullHp.ToString();
        charaImage.sprite = UnitSprites[status.unitNum];
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
