using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour
{
    /// <singletone>
    static public CostManager instance = null;
    /// <singletone>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    Image costBar;
    public float currentCost;

    void Start()
    {
        currentCost = 0f;
        this.gameObject.transform.SetAsLastSibling(); //코스트바 draw 위치 맨위로 초기화
        costBar = GetComponent<Image>();
    }

    void Update()
    {
        CostController();
        costBar.fillAmount = currentCost / 10f;
    }

    void CostController()
    {
        currentCost += Time.deltaTime / 2f;
        if (currentCost > 10f)
        {
            currentCost = 10f;
        }
    }
}
