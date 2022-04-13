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

    //��: �����Ҵ�(������ ���Ҷ� �Ҵ�ǰ� ������ ���Ҷ� �����ϴ°�)
    //����: ���������� �Ҵ�Ǵ°�
    //�����Ϳ���: ���������� ����(�Ҵ�)�Ǹ� ���α׷��� ����Ǿ� �����. static
}
