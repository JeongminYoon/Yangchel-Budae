using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class UnitSpawnEffect : MonoBehaviour
{
    float timer = 2f;
    public GameObject circlePrefab;
    public GameObject fxEffect;
    GameObject circle;
    GameObject unitModelInvis;
    GameObject unitModel;
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
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if (timer <=1f && timer > 0f)
        {
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

    public void UnitSpawnEftSetting(UnitClass unitnum, Vector3 ps, GameObject invModel)
    {
        num = unitnum;
        pos = ps;
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
