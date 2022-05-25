using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject CardSelectMenu;
    public GameObject[] LightImages = new GameObject[2];

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

    }

    // Update is called once per frame
    void Update()
    {
        switch (menu)
        {
            case MainMenu.BattleMenu:
                BattleMenu.SetActive(true);
                CardSelectMenu.SetActive(false);
                LightImages[0].SetActive(true);
                LightImages[1].SetActive(false);
                break;
            case MainMenu.CardSelectMenu:
                BattleMenu.SetActive(false);
                CardSelectMenu.SetActive(true);
                LightImages[0].SetActive(false);
                LightImages[1].SetActive(true);
                break;
            case MainMenu.End:
                BattleMenu.SetActive(false);
                CardSelectMenu.SetActive(false);
                break;
        }
    }

    public void ChangeMenu(MainMenu i)
    {
        switch (i)
        {
            case MainMenu.BattleMenu:
                menu = i;
                break;
            case MainMenu.CardSelectMenu:
                menu = i;
                break;
            case MainMenu.End:
                menu = i;
                break;
        }
    }

    public void ChangeBattleMenu()
    {
        menu = MainMenu.BattleMenu;
    }

    public void ChangeCardSelectMenu()
    {
        menu = MainMenu.CardSelectMenu;
    }
}
