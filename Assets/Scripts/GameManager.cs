using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Enums;
using Cinemachine;

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

    private bool isEndingProduct = false;
    private float productTime = 15f;
    public GameObject vtCamPrefab = null;
    //public GameObject followObjPrefab = null;
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
            if (AudioManager.instance.winAudio)
            {//0521근희 인게임에서 바로 시작시 오류 때문에 처리
                AudioManager.instance.inGameAudios[3] = AudioManager.instance.winAudio;
            }
        }
        else 
        {//우리팀 넥서스가 부서졌을 경우 -> 게임 Defect
            if(AudioManager.instance.loseAudio)
            {  //0521근희 인게임에서 바로 시작시 오류 때문에 처리
            AudioManager.instance.inGameAudios[3] = AudioManager.instance.loseAudio;
             }
        }

        isGameWin = isNexusEnemy;
        //StartCoroutine(DelayGameEnd());
        UnitManager.instance.GameEnd(isNexusEnemy);

        //연출 추가해주기
        //1. 1초 기다렸다가 fade In-Out들어가기
        //2. 카메라 세팅(원근투영+위치)
        //Camera.main.orthographic = false;
        //3. 카메라 무빙 시작 (줌 인)
        //4. 줌인되고 대기
        //5. 공전
        //6. 페이드 아웃 시작
        //7. 신 교체하고 
        //8. 페이드 인

        float fadeOutTime = 2f;
        float fadeInDelayTime = 2.5f;
        float fadeInTime = 3f;

        ///StartCoroutine(DelayGameEnd());
        ScreenFadeManager.Instance.PlayFadeOut(fadeOutTime);
        ScreenFadeManager.Instance.PlayDelayFadeIn(fadeInDelayTime, fadeInTime);

        Invoke("GameEndCamMove", fadeOutTime);
        //Invoke("DelayGameEnd", productTime);
    }

    public void GameEndCamMove(/*bool isNexusEnemy*/)
    {

        Camera.main.orthographic = false;

        CameraShake shakeScript = Camera.main.gameObject.GetComponent<CameraShake>();
        shakeScript.enabled = false;

        GameObject randObj;
        List<GameObject> list = UnitManager.instance.GetUnitList_Val(Defines.ally);
        randObj = list[Random.Range(0, list.Count)];
        //유닛 하나도 없을 떄 에외처리 필요.

        GameObject followObj = new GameObject("followObj");
        followObj.transform.position = Camera.main.transform.position;
        followObj.transform.rotation = Camera.main.transform.rotation;
        CamMove script = followObj.AddComponent<CamMove>();

        //GameObject followObj = Instantiate(followObjPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
        //CamMove script = followObj.GetComponent<CamMove>();
        script.Desc(productTime, 1f, 10f);
        script.targetObj = randObj.GetComponent<Units>().center;

        GameObject vtCam = Instantiate(vtCamPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
        //vtCam.transform.forward = Camera.main.transform.forward;

        CinemachineVirtualCamera cvc = vtCam.GetComponent<CinemachineVirtualCamera>();
        cvc.Follow = followObj.transform;
        cvc.LookAt = followObj.transform;
        //cvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = 0f;

        //StartCoroutine(DelayCinemachinSetting(vtCam));
    }

	public IEnumerator DelayGameEnd()
    {
        yield return new WaitForSeconds(productTime);
        
        StartCoroutine(ScreenFadeManager.Instance.StageEndFadeOut(2f));
        
        //ScreenFadeManager.Instance.StageEndFadeOut(2f);
        //SceneChange(Enums.SceneNum.Result);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        curScene = (SceneNum)SceneManager.GetActiveScene().buildIndex;

        if (curScene == SceneNum.InGame)
        {
            Camera.main.transform.rotation = Quaternion.Euler(Defines.camRot);
            Camera.main.transform.position = Defines.camPos;
            Camera.main.orthographic = true;

            if (!vtCamPrefab)
            {
                vtCamPrefab = Resources.Load("Prefabs/EndingVirtualCam") as GameObject;
            }

            //if (!followObjPrefab)
            //{
            //    followObjPrefab = Resources.Load("Prefabs/followObj") as GameObject;
            //}
        }

   
        //AudioManager.instance.BGMPlay(curScene);
    }

    void Start()
    {
        isGameEnd = false;

        AudioManager.Instance.BGMPlay(curScene);
        bgmSlider = setting_Menu.transform.Find("BGM_Slider").GetComponent<Slider>();
        bgmSlider.value = 0.5f;
        sfxSlider = setting_Menu.transform.Find("FX_Slider").GetComponent<Slider>();
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
