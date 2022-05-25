using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUiManager : MonoBehaviour
{
    #region singletone
    static public MainMenuUiManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public GameObject BattleMenu;
    public GameObject BattleButton;
    public GameObject CardSelectMenu;
    public GameObject Menus;
    public GameObject[] LightImages = new GameObject[2];
    public GameObject ramp;
    public Sprite[] RampImages = new Sprite[2];
    public GameObject BattleButtonInfo;
    public RectTransform[] MovePos = new RectTransform[2];
    public GameObject alarm0;
    public GameObject alarm1;

    public enum MainMenu
    {
        BattleMenu = 0,
        CardSelectMenu = 1,
        End = 2
    }

    [SerializeField]
    MainMenu menu = MainMenu.BattleMenu;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMenu(MainMenu.BattleMenu);
    }

    bool triger = false;
    // Update is called once per frame
    void Update()
    {
        MenuEffect();
    }
    float timer = 1f;
    bool timertrg = false;
    void MenuEffect()
    {
        GameObject go = LightImages[0]; ;
        switch (menu)
        {
            case MainMenu.BattleMenu:
                go = LightImages[0];
                break;
            case MainMenu.CardSelectMenu:
                go = LightImages[1];
                break;
        }

        go.GetComponent<Image>().color = new Color(1,1,1,timer);

        if (timer > 0.5f && timertrg == false)
        {
            timer -= Time.deltaTime / 2;
        }
        else
        {
            timertrg = true;
            timer += Time.deltaTime / 2;
            if (timer >= 1f)
            {
                timertrg = false;
            }
        }
    }

    public void ChangeMenu(MainMenu i)
    {
        menu = i;
        switch (i)
        {
            case MainMenu.BattleMenu:
                iTween.MoveTo(Menus, iTween.Hash("x", MovePos[0].position.x, "easeType", "easeOutBounce", "time", 0.7f));
                //BattleMenu.SetActive(true);
                //CardSelectMenu.SetActive(false);
                LightImages[0].SetActive(true);
                LightImages[1].SetActive(false);
                break;
            case MainMenu.CardSelectMenu:
                iTween.MoveTo(Menus, iTween.Hash("x", MovePos[1].position.x, "easeType", "easeOutBounce", "time", 0.7f));
                triger = true;
                    //BattleMenu.SetActive(false);
                    //CardSelectMenu.SetActive(true);
                    LightImages[0].SetActive(false);
                    LightImages[1].SetActive(true);
                break;
            case MainMenu.End:
                BattleMenu.SetActive(false);
                CardSelectMenu.SetActive(false);
                break;
        }
    }

    public void ChangeBattleMenu()
    {
        ChangeMenu(MainMenu.BattleMenu);
        //iTween.MoveTo()
    }

    public void ChangeCardSelectMenu()
    {
        ChangeMenu(MainMenu.CardSelectMenu);
    }


    public void BattleButtonEnable(int i)
    {
        if (i == 0)
        {
            BattleButton.GetComponent<NextButton>().ChangeButtonColor(i);
            BattleButtonInfo.SetActive(true); 
        }
        else
        {
            BattleButton.GetComponent<NextButton>().ChangeButtonColor(i);
            BattleButtonInfo.SetActive(false);
        }
    }

    public void SetRamp(int i)
    {
        ramp.GetComponent<Image>().sprite = RampImages[i];
    }

    public void SetCardAlarm(int cardNum, bool hide)
    {
        alarm0.transform.Find("Text_Alram").gameObject.GetComponent<Text>().text = cardNum.ToString();
        alarm0.SetActive(hide);
        alarm1.SetActive(!hide);
    }
}
