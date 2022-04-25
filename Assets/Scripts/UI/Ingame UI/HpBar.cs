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

    public GameObject Unit = null;
    UnitStatus status = null;

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

        hpText.text = (currentHp).ToString();
        transform.position = cam.WorldToScreenPoint(pos1 + new Vector3(0f, 3.2f, 0f));
        hpBarImage.fillAmount = currentHp / fullHp;
        DamageEffect();
        HpBarSetting();

        if (eftSwitch == false)
        {
            hpBarEftValu = fullHp;
            eftSwitch = true;
        }
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

    void HpBarSetting()
    {
        if (Unit != null)
        {
            status = Unit.GetComponent<Units>().unitStatus;
            currentHp = status.curHp;
            fullHp = status.fullHp;
            pos1 = Unit.transform.position;
            if (Unit.GetComponent<Units>().isEnemy == true)
            {
                hpBarImage.sprite = hpBarSprite[1];
            }
            else
            {
                hpBarImage.sprite = hpBarSprite[0];
            }


            if (currentHp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    //public void HpBarSetting(float curHp, float fulHp, Vector3 pos, bool isEnemy = false)
    //{
    //    currentHp = curHp;
    //    fullHp = fulHp;
    //    pos1 = pos;
    //    if (isEnemy == true)
    //    {
    //        hpBarImage.sprite = hpBarSprite[1];
    //    }
    //    else
    //    {
    //        hpBarImage.sprite = hpBarSprite[0];
    //    }
    //}

    //��: �����Ҵ�(������ ���Ҷ� �Ҵ�ǰ� ������ ���Ҷ� �����ϴ°�)
    //����: ���������� �Ҵ�Ǵ°�
    //�����Ϳ���: ���������� ����(�Ҵ�)�Ǹ� ���α׷��� ����Ǿ� �����. static
}
