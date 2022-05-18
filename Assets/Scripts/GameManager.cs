﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


            //sound
            AudioManager.instance.BGMPlay(sceneNum);

            

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

            AudioManager.instance.inGameAudios[3] = AudioManager.instance.winAudio;
        }
        else 
        {//우리팀 넥서스가 부서졌을 경우 -> 게임 Defect
            AudioManager.instance.inGameAudios[3] = AudioManager.instance.loseAudio;
        }
       // AudioManager.instance.BGMPlay(Enums.SceneNum.Result);

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
        //AudioManager.instance.BGMPlay(curScene);
    }

    void Start()
    {
        isGameEnd = false;

        AudioManager.instance.BGMPlay(curScene);
        bgmSlider = setting_Menu.transform.Find("FX_Slider").GetComponent<Slider>();
        bgmSlider.value = 0.5f;
        sfxSlider = setting_Menu.transform.Find("BGM_Slider").GetComponent<Slider>();
        sfxSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))   //Save(씬 탈출할때 여기 내용물 실행시킬것)
        {
            print(MyHandsList.Count);
        }

        if (bgmSlider != null)
        {//오류 수정 : 0518근희
            AudioManager.instance.bgmSoundSet(bgmSlider.value);
        }
        if (sfxSlider != null)
        {
            AudioManager.instance.sfxSoundSet(sfxSlider.value);
        }
    }


    #region 게임 설정창 변수/함수들 feat.Yoon
    public GameObject setting_Menu;
    Slider bgmSlider;
    Slider sfxSlider;

    public void SetMenuActive(bool isActive)
    {
        if (isActive == false)
        {
            setting_Menu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            setting_Menu.SetActive(true);
            Time.timeScale = 0;
        }    
    }

    #endregion
}
