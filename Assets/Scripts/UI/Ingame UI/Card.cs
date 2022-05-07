using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Structs;

public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public UnitStatus status;
    public GameObject spawnEffect;
    public Text unitName;
    public Text unitCost;
    public int cardDeskPos;
    Vector3 cardPos;

    public int value;

    public bool isenabled;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        unitName = this.gameObject.transform.Find("Name").gameObject.GetComponent<Text>();
        unitCost = this.gameObject.transform.Find("Cost").gameObject.GetComponent<Text>();


        unitName.text = status.unitName;
        unitCost.text = (status.cost).ToString();
    }

    RectTransform rt1;
    RectTransform rt2;
    Vector3 Rt1;
    GameObject unitModel;
    Material unitModelMat;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {//�巡�� ���� ������
        //ī���� ó����ġ cardPos�� ������
        cardPos = transform.position;

        //ī�� �ִϸ��̼� ���� ����
        rt1 = GetComponent<RectTransform>();
        rt2 = GetComponent<RectTransform>();
        Rt1 = rt1.anchoredPosition;
        unitModel =  Instantiate(NewCardManager.instance.unitModels[status.unitNum]);
        unitModelMat = unitModel.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials[0];
        unitModelMat.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        unitModelMat.color = new Color(unitModelMat.color.r, unitModelMat.color.g, unitModelMat.color.b, 0.5f);

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {//�巡�� ��
        Vector3 currentPos = eventData.position;
        transform.position = currentPos;
        //ī�� �ִϸ��̼� ���
        CardAnim();
        SpawnRange.instance.ShowSpawnRangeEffect();

        RayResult temp = Funcs.RayToWorld();
        if (temp.isHit == false || temp.hitObj.tag == "Tower" || temp.hitObj.tag == "Nexus" || temp.hitObj.tag == "SpawnRange")
        {
            unitModel.SetActive(false);
        }
        else
        {
            unitModel.SetActive(true);
            unitModel.transform.position = temp.hitPosition + new Vector3(0f,0.55f);
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {//����Ҷ�
        RayResult temp = Funcs.RayToWorld();

        if (this.status.unitName.Contains("Skill") && float.Parse(unitCost.text) < CostManager.instance.currentCost && temp.isHit == true) //����� ī�尡 ��ų�̸�
        {
            SpawnSkill();
            NewCardManager.instance.SpawnCard(this.gameObject, 4);
            NewCardManager.instance.CardUse(this.gameObject);
            CostManager.instance.currentCost -= float.Parse(unitCost.text);
        }
        else //����� ī�尡 �����̸�
        {
            if (temp.isHit == false)
            {
                //���ΰ��� ���� �ƴϸ� ī���� ó����ġ(cardPos)�� ī�带 �ǵ�������
                transform.position = cardPos;
                Destroy(unitModel);
                print("not ground");
            }
            else
            {   //���ΰ��� ���̸� ���� ��ȯ

                if (temp.hitObj.tag == "Tower" || temp.hitObj.tag == "Nexus" || temp.hitObj.tag == "SpawnRange" || float.Parse(unitCost.text) > CostManager.instance.currentCost)
                { //���ΰ��� Ÿ��,�ؼ����� ��ȯ ���
                    transform.position = cardPos;
                    Destroy(unitModel);
                }
                else
                {
                    //UnitFactory.instance.SpawnUnit((Enums.UnitClass)status.unitNum, temp.hitPosition);
                    GameObject spawnEft = Instantiate(spawnEffect);
                    spawnEft.GetComponent<UnitSpawnEffect>().UnitSpawnEftSetting((Enums.UnitClass)status.unitNum, temp.hitPosition, status.cost, unitModel);
                   
                    NewCardManager.instance.SpawnCard(this.gameObject, 4);
                    NewCardManager.instance.CardUse(this.gameObject);
                    CostManager.instance.currentCost -= float.Parse(unitCost.text);
                }
            }
        }
        //ī�� �ִϸ��̼� �ʱ�ȭ
        transform.localScale = new Vector3(1, 1, 1);
        SpawnRange.instance.HideSpawnRangeEffect();
    }

    void CardAnim()
    {
        float h = (rt2.anchoredPosition.y - Rt1.y) / 219;
        if (h >= 1)
        {
            h = 1;
        }
        if (h <= 0)
        {
            h = 0;
        }
        transform.localScale = new Vector3(1 - h, 1 - h, 1);
    }

    void SpawnSkill()
    {
        //Instantiate(UnitFactory.instance.unitPrefabs[status.unitNum]);
        if (this.status.unitNum == 6)
        {
            SkillManager.instance.UseSkill1();
        }
        else if (this.status.unitNum == 7)
        {
            if (SkillManager.instance.isSkill2Live == false)
            {
                SkillManager.instance.UseSkill2();
            }
        }
    }
    public void DeletUnitModel(GameObject go)
    {
        Destroy(go);
    }
}
