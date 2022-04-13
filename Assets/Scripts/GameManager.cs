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
        //-> �� ���
        //-> Ÿ�����
        //-> ���� �Ŵ��� ����
        //-> �ٸ� �Ŵ��� ����
        //-> hp bar manager ����
    }

    public void SceneChange(int sceneNum)
    {

        //���� �ٲ���ڵ�
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
        if (Input.GetMouseButtonDown(1))   //Save(�� Ż���Ҷ� ���� ���빰 �����ų��)
        {
            print(MyHandsList.Count);
        }
    }
}
