using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class UnitSpawnEffect : MonoBehaviour
{
    public float timer = 1f;
    public GameObject circlePrefab;
    GameObject circle;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (pos != null && swt == false)
        {
            swt = true;
            circle = Instantiate(circlePrefab, cam.WorldToScreenPoint(pos), Quaternion.identity, canvasTr);
        }
        timer -= Time.deltaTime;
        circle.GetComponent<Image>().fillAmount = timer;
        if (timer <= 0f)
        {
            UnitFactory.instance.SpawnUnit(num, pos);
            Destroy(circle);
            Destroy(this.gameObject);
        }
    }

    public void UnitSpawnEftSetting(UnitClass unitnum, Vector3 ps)
    {
        num = unitnum;
        pos = ps;
    }
}
