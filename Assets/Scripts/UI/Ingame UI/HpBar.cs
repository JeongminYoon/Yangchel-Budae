using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    Text hpText;
    Camera cam = null;
    public int currentHp = -999;
    public int fullHp;
    public Vector3 pos1;

    void Start()
    {
        cam = Camera.main;
        hpText = transform.Find("HpText").gameObject.GetComponent<Text>();
    }

    void Update()
    {
        
        hpText.text = (currentHp + "/" + fullHp).ToString();
        transform.position = cam.WorldToScreenPoint(pos1 + new Vector3(0f, 3.2f, 0f));
        if (currentHp != -999 && currentHp <= 0)
        {
            Destroy(this);
        }
    }

    public void HpBarSetting(int curHp, int fulHp, Vector3 pos)
    {
        currentHp = curHp;
        fullHp = fulHp;
        pos1 = pos;
    }

    //힙: 동적할당(유저가 원할때 할당되고 유저가 원할때 해제하는것)
    //스택: 지역에서만 할당되는것
    //데이터영역: 선언됬을때 실행(할당)되며 프로그램이 종료되야 사라짐. static
}
