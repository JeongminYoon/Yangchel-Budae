using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    /// <singletone>
    static public CardManager instance = null;
    /// <singletone>

    public List<UnitStatus> allUnitStatus = new List <UnitStatus>();
    List<UnitStatus> unitStatusList = new List <UnitStatus>();

    List<GameObject> AllCard = new List<GameObject>();
    public GameObject[] myCard;
    public GameObject cardPrefab;
    public GameObject Pos0;

    public GameObject uiCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        unitStatusList = allUnitStatus;
        myCard = new GameObject[unitStatusList.Count];
        for (int i = 0; i < myCard.Length; i++)
        {
            myCard[i] = Instantiate(cardPrefab, uiCanvas.transform);
            myCard[i].GetComponent<CardPrefab>().status = ScriptableObject.CreateInstance<UnitStatus>();
            myCard[i].GetComponent<CardPrefab>().status.DeepCopy(unitStatusList[i]);
            //RectTransform rt = Find
            RectTransform CardRt = myCard[i].GetComponent<RectTransform>();
            CardRt.anchoredPosition = SortCard(Pos0, i);
        }
    }

    void Update()
    {
        
    }


    Vector2 SortCard(GameObject Pos, int i)
    {
        int num = i + 1;
        int w=0;
        int h = Mathf.Abs(i / 4);
        if (num > 4)
        {
            w = num - 4*h;
        }
        else
        {
            w = num;
        }
        Vector2 result = Pos.GetComponent<RectTransform>().anchoredPosition + new Vector2(w * 260 - 260, 0 - h * 390);
        return result;
    }


}
