using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Structs;
using TMPro;

public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnitStatus status;
    public GameObject spawnEffect;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitCost;
    public TextMeshProUGUI unitHp;
    public Image charaImage;
    public Image frameImage;
    public Image glowImage;
    public Image typeImage;
    public int cardDeskPos;
    Vector3 cardPos;
    Color defaltCol;

    public Sprite[] UnitSprites = new Sprite[6];
    public Sprite[] unitTypeSprites = new Sprite[4]; //melee, range, utility, skill
    public Sprite[] cardFrames = new Sprite[2];

    Rect deskRect;


	private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (status.unitName.Contains("Skill"))
        {
            frameImage.sprite = cardFrames[1];
            glowImage.color = new Color(0.73f, 0.26f, 0.77f);
        }
        else
        {
            frameImage.sprite = cardFrames[0];
            glowImage.color = new Color(0f, 0.69f, 0.44f);
        }

        if (status.unitName.Contains("Melee"))
        {
            typeImage.sprite = unitTypeSprites[0];
        }
        else if (status.unitName.Contains("Range"))
        {
            typeImage.sprite = unitTypeSprites[1];
        }
        else if (status.unitName.Contains("Skill"))
        {
            typeImage.sprite = unitTypeSprites[3];
        }
        else
        {
            typeImage.sprite = unitTypeSprites[2];
        }

        unitName.text = status.unitName;
        unitCost.text = (status.cost).ToString();
        unitHp.text = status.fullHp.ToString();
        charaImage.sprite = UnitSprites[status.unitNum];



        //Deck UI 사이즈 받아오는곳.
        //=> Unity는 좌하단이 0,0 
        //그리고 덱 UI는 우측하단 기준 고정임 => 그래서 요렇게
        GameObject cardDeck = GameObject.Find("CardDesk");
        float deckWidth = cardDeck.GetComponent<RectTransform>().rect.width;
        float deckHeight = cardDeck.GetComponent<RectTransform>().rect.height;

        deskRect.xMax = Defines.winCX;
        deskRect.xMin = Defines.winCX - deckWidth;
        deskRect.yMin = 0f;
        deskRect.yMax = deckHeight;

    }

	public void Update()
	{
        //if (animState == eCardAnimState.PopEnd)
        //{
        //    if (!EventSystem.current.IsPointerOverGameObject())
        //    {
        //        //
        //        CardPopDownAnim();
        //    }
        //}
    }

	RectTransform rt1;
    RectTransform rt2;
    Vector3 vec;
    GameObject unitModel;
    public GameObject unitPlatformPrefab;
    GameObject unitPlatform;
    Material unitModelMat;
    
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {//드래그 시작 했을때
     //카드의 처음위치 cardPos를 정해줌
        cardPos = transform.position;

        //카드 애니메이션 변수 선언
        rt1 = GetComponent<RectTransform>();
        rt2 = GetComponent<RectTransform>();
        vec = rt1.anchoredPosition;

        if (this.status.unitName.Contains("Skill"))
        {
        }
        else
        {
            unitModel = Instantiate(NewCardManager.instance.unitModels[status.unitNum]);
            unitModelMat = unitModel.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials[0];
            unitModelMat.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            defaltCol = new Color(unitModelMat.color.r, unitModelMat.color.g, unitModelMat.color.b, 0.5f);
            unitModelMat.color = defaltCol;

            unitPlatform = Instantiate(unitPlatformPrefab, new Vector3(999, 999, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
            unitPlatform.GetComponent<SpriteRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            print(unitPlatform.GetComponent<SpriteRenderer>().material.color);
        }

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {//드래그 중
        Vector3 currentPos = eventData.position;
        transform.position = currentPos;
        //카드 애니메이션 재생
        CardAnim();
        SpawnRange.instance.ShowSpawnRangeEffect();

        if (this.status.unitName.Contains("Skill"))
        {
        }
        else 
        {
            RayResult temp = Funcs.RayToWorld();
            if (temp.isHit == false || temp.hitObj.tag == "Tower" || temp.hitObj.tag == "Nexus" || temp.hitObj.tag == "SpawnRange")
            {
                //unitModel.SetActive(false);
                unitModelMat.color = new Color(unitModelMat.color.r,0, 0, 0.5f);
                unitPlatform.GetComponent<SpriteRenderer>().material.color = new Color(0.5f, 0.2f, 0.2f, 0.5f);
            }
            else
            {
                //unitModel.SetActive(true);
                unitModelMat.color = defaltCol;
                unitPlatform.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 0.5f);
                unitModel.transform.position = temp.hitPosition + new Vector3(0f, 0.55f);
                unitPlatform.transform.position = temp.hitPosition + new Vector3(0f, 0.65f);
            }
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {//드롭할때

        //0525 전근희
        //카드 다시 제자리 돌려 놓을 때 소환되는 버그 수정 시작
        // 드래그앤 드롭 끝났을때 마우스 위치 확인해서 
        //1. 먼저 카드 데스크 위면 소환xxx 아니면 소환 ㄱㄱ

        Vector3 mousePos = Input.mousePosition;
        //Vector3 cursorPos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        Debug.Log("MousePos : " + mousePos);

        if ((mousePos.x <= deskRect.xMax && mousePos.x >= deskRect.xMin)
            &&
            (mousePos.y <= deskRect.yMax && mousePos.y >= deskRect.yMin))
        {
            transform.localScale = new Vector3(0.38f, 0.38f, 0f);
            transform.position = cardPos;
            Destroy(unitModel);
            Destroy(unitPlatform);

            return;
        }

        RayResult temp = Funcs.RayToWorld();

        if (this.status.unitName.Contains("Skill") && cardAnim_h ==1f && float.Parse(unitCost.text) < CostManager.instance.currentCost && temp.isHit == true) //사용한 카드가 스킬이면
        {
            SpawnSkill();
            NewCardManager.instance.SpawnCard(this.gameObject, 4);
            NewCardManager.instance.CardUse(this.gameObject);
            CostManager.instance.currentCost -= float.Parse(unitCost.text);
        }
        else //사용한 카드가 유닛이면
        {
            if (temp.isHit == false)
            {
                //닿인곳이 땅이 아니면 카드의 처음위치(cardPos)로 카드를 되돌려놓음
                transform.position = cardPos;
                Destroy(unitModel);
                Destroy(unitPlatform);
                print("not ground");
            }
            else
            {   //닿인곳이 땅이면 유닛 소환
                if (temp.hitObj.tag == "Tower" || temp.hitObj.tag == "Nexus" || temp.hitObj.tag == "SpawnRange" || float.Parse(unitCost.text) > CostManager.instance.currentCost ||  temp.hitObj.tag != "MapData")
                { //닿인곳이 타워,넥서스면 소환 취소
                    transform.position = cardPos;
                    Destroy(unitModel);
                    Destroy(unitPlatform);
                }
                else
                {
                    print(temp.hitObj.tag);
                    //UnitFactory.instance.SpawnUnit((Enums.UnitClass)status.unitNum, temp.hitPosition);
                    GameObject spawnEft = Instantiate(spawnEffect);
                    spawnEft.GetComponent<UnitSpawnEffect>().UnitSpawnEftSetting((Enums.UnitClass)status.unitNum, temp.hitPosition, status.cost, unitModel);
                    Destroy(unitPlatform);

                    NewCardManager.instance.SpawnCard(this.gameObject, 4);
                    NewCardManager.instance.CardUse(this.gameObject);
                    CostManager.instance.currentCost -= float.Parse(unitCost.text);
                }
            }
        }
        //카드 애니메이션 초기화
        transform.localScale = new Vector3(0.38f, 0.38f, 0.38f);
        SpawnRange.instance.HideSpawnRangeEffect();
    }


    float cardAnim_h;
    void CardAnim()
    {
        cardAnim_h = (rt2.anchoredPosition.y - vec.y) / 219;
        if (cardAnim_h >= 1)
        {
            cardAnim_h = 1;
        }
        if (cardAnim_h <= 0)
        {
            cardAnim_h = 0;
        }
        transform.localScale = new Vector3((1 - cardAnim_h) * 0.38f, (1 - cardAnim_h) * 0.38f, 1);
        print(cardAnim_h);
    }

    public void CardPopUpAnim(int i)
    {
        if (i == 0)
        {
            IEnumerator ie = PopUpAnim(1.5f);
            StartCoroutine(ie);
            //iTween.MoveBy(gameObject, iTween.Hash("y", 5, "sin", "easeInOutExpo"));
        }
        else
        {
			IEnumerator ie = PopUpAnim(-1.5f);
			StartCoroutine(ie);
			//iTween.MoveBy(gameObject, iTween.Hash("y", -5, "sin", "easeInOutExpo"));
		}
    }

    //test


    IEnumerator PopUpAnim(float power)
    {
        for (int i = 0; i <= 10; i++)
        {
            this.gameObject.GetComponent<RectTransform>().position += new Vector3(0f, power, 0f);
            
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //카드위에 마우스 올렸을 때
        
        CardPopUpAnim(0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardPopUpAnim(1);
    }


    //private void OnMouseExit()
    //{
    //    isAnim_PopUp_Played = false;
    //    CardPopUpAnim(1);
    //}


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
