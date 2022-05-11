using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;

public class GameManager : MonoBehaviour
{
    //GameManager 겸 SceneManager


    /// <singletone>
    static public GameManager instance = null;
    /// <singletone>



    static public List<UnitStatus> MyHandsList = new List<UnitStatus>();
    //For Selected Cards, from CardSelectScene To InGame
    

    static GameObject Card2;


    public SceneNum curScene = SceneNum.SceneEnd;
    public SceneNum pastScene = SceneNum.SceneEnd;
    public SceneNum nextScene = SceneNum.SceneEnd;

    public bool isGameWin = true;
    public bool isGameEnd = false;
    public void InGameSceneSetting()
    { 
        //-> 맵 깔기
        //-> 타워깔기
        //-> 유닛 매니저 세팅
        //-> 다른 매니저 세팅
        //-> hp bar manager 세팅
    }

    public void SceneChange(SceneNum sceneNum)
    {
        //Debug.Log(sceneNum + "으로 신 바꾸라는 명령");
        nextScene = sceneNum;

        if (curScene != nextScene && nextScene != SceneNum.SceneEnd)
        {
            pastScene = curScene;
            curScene = sceneNum;
            nextScene = SceneNum.SceneEnd;
           //Debug.Log(sceneNum + "으로 신 바꾸기 성공");
            SceneManager.LoadScene((int)sceneNum);
        }
        else
        {
            //Debug.Log(sceneNum + "으로 신 바꾸기 실패");
            return;
        }
    }


    

    public void GameStart()
    {
        isGameEnd = false;
        SceneChange(SceneNum.CardSelect);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); //겜 프로그램 시마이
#endif
    }

    public void InGameResultCheck(bool isNexusEnemy)
    { //넥서스 부서지면 자동으로 호출될 함수.

        isGameEnd = true;

        if (isNexusEnemy)
        { //적팀 넥서스가 부서졌을 경우 -> 게임 승리 WIN
        }
        else 
        {//우리팀 넥서스가 부서졌을 경우 -> 게임 Defect
        }

        isGameWin = isNexusEnemy;
        StartCoroutine(DelayGameEnd());
        UnitManager.instance.GameEnd(isNexusEnemy);

        //연출 추가해주기

    }

    public IEnumerator DelayGameEnd()
    {

        yield return new WaitForSecondsRealtime(5f);
        SceneChange(Enums.SceneNum.Result);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        curScene = (SceneNum)SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {

        isGameEnd = false;
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
