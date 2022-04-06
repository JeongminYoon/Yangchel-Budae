using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUIManager : MonoBehaviour
{
    ///singletone
    static public DamageUIManager instance = null;
    ///singletone
   
    Camera cam = null;
    public GameObject hpEffectPrefab;
    Text hpText;

    //�׽�Ʈ��. ���߿� ��Կ��� �� ���� �ޱ�.
    Vector3 unitPositionTest = new Vector3(0, 0, 0);
    int unitDamageTest = 5;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayHpEffect(unitDamageTest, unitPositionTest);
        }

    }

    public GameObject PlayHpEffect(int damage, Vector3 worldPos)
    {
        GameObject hpEffect = Instantiate(hpEffectPrefab, unitPositionTest, Quaternion.identity, transform);
        hpEffect.transform.position = cam.WorldToScreenPoint(worldPos);
        hpText = hpEffect.GetComponent<Text>();
        hpText.text = damage.ToString();

        return hpEffect;
    }
}
