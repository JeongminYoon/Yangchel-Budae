using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class UnitSpawnEffect : MonoBehaviour
{
    float timer = 2f;
    public GameObject circlePrefab;
    public GameObject costEffectPrefab;
    public GameObject fxEffect;
    GameObject circle;
    GameObject unitModelInvis;
    GameObject unitModel;
    Text costEffectText;
    int cost;
    UnitClass num;
    Vector3 pos;
    bool swt = false;
    Transform canvasTr;
    Camera cam = null;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        canvasTr = GameObject.Find("UI").transform.Find("Canvas");
        circle = Instantiate(circlePrefab, cam.WorldToScreenPoint(pos), Quaternion.identity, canvasTr);
        circle.SetActive(false);
        CostEffectSetting();
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if (timer > 1f)
        {
            CostEffectPlay();
        }
        else if (timer <=1f && timer > 0f)
        {
            Destroy(costEffect);
            if (swt == false)
            {
                swt = true;
                Destroy(unitModelInvis);
                MoveUnitModel();
            }
            circle.GetComponent<Image>().fillAmount = timer;
        }
        else if (timer <= 0f)
        {
            Destroy(unitModel);
            Destroy(circle);
            UnitFactory.instance.SpawnUnit(num, pos);
            Destroy(this.gameObject);
        }  
    }

    GameObject costEffect;
    RectTransform rt;
    void CostEffectSetting()
    {
        costEffect = Instantiate(costEffectPrefab, cam.WorldToScreenPoint(pos), Quaternion.identity, canvasTr);
        costEffectText = costEffect.transform.Find("Text").GetComponent<Text>();
        costEffectText.text = cost.ToString();
        rt = costEffect.GetComponent<RectTransform>();
        rt.anchoredPosition += new Vector2(0f, 60f);
        //rt.sizeDelta = new Vector2(0f, 0f);
    }

    float eftTimer = 0f;
    void CostEffectPlay()
    {
        eftTimer += Time.deltaTime;
        if (eftTimer >= 0.05f)
        {
            eftTimer = 0f;
            rt.anchoredPosition += new Vector2(0f, 1f);
            //rt.sizeDelta += new Vector2(3.5f, 3.5f);
            costEffect.GetComponent<Image>().color -= new Color(0f, 0f, 0f, 0.07f);
            costEffectText.color -= new Color(0f, 0f, 0f, 0.07f);
        }
    }

    public void UnitSpawnEftSetting(UnitClass unitnum, Vector3 ps, int cst,GameObject invModel)
    {
        num = unitnum;
        pos = ps;
        cost = cst;
        unitModelInvis = invModel;
    }

    void MoveUnitModel()
    {
        circle.SetActive(true);
        unitModel = Instantiate(NewCardManager.instance.unitModels[(int)num], pos, Quaternion.identity);
        unitModel.transform.position += new Vector3(0, 10f, 0);
        iTween.MoveTo(unitModel, iTween.Hash("islocal", true, "y", pos.y + 0.6f, "time", 0.95f, "easetype", "easeOutBounce", "oncompletetarget", this.gameObject));
        Instantiate(fxEffect, pos, Quaternion.identity);
    }
}
