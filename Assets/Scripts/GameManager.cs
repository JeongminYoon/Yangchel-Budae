using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <singletone>
    static public GameManager instance = null;
    /// <singletone>

    static public List<UnitStatus> MyHandsList = new List<UnitStatus>();

    static GameObject Card2;

    public enum Scenes
    { 
        MainMenu = 0,
        CardSelect,
        InGame,
        GameResult
    }

    public Scenes curScene;
    public Scenes nextScene;

    public void InGameSceneSetting()
    { 
        //-> 맵 깔기
        //-> 타워깔기
        //-> 유닛 매니저 세팅
        //-> 다른 매니저 세팅
        //-> hp bar manager 세팅
    }

    public void SceneChange(int sceneNum)
    {

        //씬이 바뀌는코드
        curScene = (Scenes)sceneNum;
        
    }
     

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {

        switch (curScene)
        {
            case Scenes.MainMenu:
                { 
                    
                }
                break;
            case Scenes.CardSelect:
                {
                    
                }
                break;
            case Scenes.InGame:
                {
                    InGameSceneSetting();
                }
                break;
            case Scenes.GameResult:
                { 
                
                }
                break;
            default:
                break;
        }

        //if (curScene == Scenes.InGame)
        //{
        //    InGameSceneSetting();
        //}

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))   //Save(씬 탈출할때 여기 내용물 실행시킬것)
        {
            print(MyHandsList.Count);
        }
    }
}
