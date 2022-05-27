using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class EnemyUnitSpawnEffect : MonoBehaviour
{
    float timer = 1f;
    public GameObject circlePrefab;
    public GameObject fxEffect;
    GameObject circle;
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

        if (timer > 0f)
        {
            if (swt == false)
            {
                swt = true;
                MoveUnitModel();
            }
            circle.GetComponent<Image>().fillAmount = timer;
        }
        else if (timer <= 0f)
        {
            Destroy(unitModel);
            Destroy(circle);
            Destroy(this.gameObject);
        }  
    }


    public void EnemyUnitSpawnEftSetting(UnitClass unitnum, Vector3 ps)
    {
        num = unitnum;
        pos = ps;
    }

    void MoveUnitModel()
    {
        circle.SetActive(true);
        unitModel = Instantiate(NewCardManager.instance.unitModels[(int)num], pos, Quaternion.identity);
        unitModel.transform.position += new Vector3(0, 10f, 0);
        unitModel.transform.eulerAngles = new Vector3(0, 180f, 0);
        iTween.MoveTo(unitModel, iTween.Hash("islocal", true, "y", pos.y + 0.6f, "time", 0.95f, "easetype", "easeOutBounce", "oncompletetarget", this.gameObject));
        StartCoroutine("PlayFx");
        print("»õ¾ù¤·");
    }

    IEnumerator PlayFx()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(fxEffect, pos, Quaternion.identity);
    }
}
