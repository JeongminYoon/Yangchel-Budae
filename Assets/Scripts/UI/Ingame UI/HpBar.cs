using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    Text hpText;
    Camera cam = null;
    public float currentHp;
    public float fullHp;
    float hpBarEftValu;
    public Vector3 pos1;
    Image hpBarImage;
    public Sprite[] hpBarSprite = new Sprite[2];

    bool eftSwitch = false;
    bool isAnimPlay = false;
    float time;

    void Start()
    {
        cam = Camera.main;
        hpText = transform.Find("HpText").GetComponent<Text>();
        hpBarImage = transform.Find("HpBarImage").GetComponent<Image>();
    }

    void Update()
    {
        if (eftSwitch == false)
        {
            hpBarEftValu = fullHp;
            eftSwitch = true;
        }

        hpText.text = (currentHp).ToString();
        transform.position = cam.WorldToScreenPoint(pos1 + new Vector3(0f, 3.2f, 0f));
        hpBarImage.fillAmount = currentHp / fullHp;
        DamageEffect();
    }

    void DamageEffect()
    {
        if (currentHp < hpBarEftValu)
        {
            hpBarEftValu = currentHp;
            isAnimPlay = true;
        }


        if (isAnimPlay == true)
        {
            time += Time.deltaTime;
            if (time < 0.5f)
            {
                hpBarImage.GetComponent<Image>().color = new Color(1, 1, 1, 1 - time);
            }
            else
            {
                hpBarImage.GetComponent<Image>().color = new Color(1, 1, 1, time);
                if (time > 1f)
                {
                    time = 0f;
                    hpBarImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    isAnimPlay = false;
                }
            }
        }
    }

    public void HpBarSetting(float curHp, float fulHp, Vector3 pos, bool isEnemy = false)
    {
        currentHp = curHp;
        fullHp = fulHp;
        pos1 = pos;
        if (isEnemy == true)
        {
            hpBarImage.sprite = hpBarSprite[1];
        }
        else
        {
            hpBarImage.sprite = hpBarSprite[0];
        }
    }

    //힙: 동적할당(유저가 원할때 할당되고 유저가 원할때 해제하는것)
    //스택: 지역에서만 할당되는것
    //데이터영역: 선언됬을때 실행(할당)되며 프로그램이 종료되야 사라짐. static
}
