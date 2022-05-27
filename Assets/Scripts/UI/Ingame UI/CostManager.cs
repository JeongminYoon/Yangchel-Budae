using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public GameObject CostBar;
    public TextMeshProUGUI costText;
    Image costBar;
    public float currentCost;

    void Start()
    {
        currentCost = 0f;
        
        //CostBar.transform.SetAsLastSibling(); //�ڽ�Ʈ�� draw ��ġ ������ �ʱ�ȭ
        costBar = CostBar.GetComponent<Image>();
        costText.text = 0.ToString();
    }

    void Update()
    {
        CostController();

        costText.text = (Mathf.Floor(currentCost)).ToString();
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
