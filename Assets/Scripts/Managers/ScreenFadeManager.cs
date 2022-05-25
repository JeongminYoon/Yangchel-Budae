using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFadeManager : MonoBehaviour
{
    //원래는 UI매니저같은ㄱ
    private static ScreenFadeManager instance = null;

    public static ScreenFadeManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject obj = new GameObject("ScreenFadeManager");
                obj.AddComponent<ScreenFadeManager>();
            }

            return instance;
        }
    }

    private float fadeInCurTime = 0f;
    private float fadeOutCurTime = 0f;

    private float startAlpha;
    private float endAlpha;

    public bool isPlaying = false;

    public delegate void finishFunc(bool UiShow);
    public finishFunc showUiDel;
    //인자, 반환값에 상관없이 못하나...?
    //아니면 진짜 그냥 함수포인터만 넘긴다던가 아 씟펄

    public Image fadeImage = null;

    public void PlayDelayFadeIn(float delayTime, float fadeTime)
    {
        StartCoroutine(DelayFade(true, delayTime, fadeTime));
    }

    public void PlayDelayFadeOut(float delayTime, float fadeTime)
    {
        StartCoroutine(DelayFade(false, delayTime, fadeTime));
    }
    public void PlayFadeIn(float fadeTime/*,finishFunc func = null*/)
    {//FadeIn = 화면 밝아지는거 
     //fadeDelegate = func;

        if (isPlaying)
        {
            return;
        }

        startAlpha = 1f;
        endAlpha = 0f;

        fadeImage.gameObject.SetActive(true);
        fadeImage.gameObject.transform.SetAsLastSibling();

        StartCoroutine(FadeIn(fadeTime));
	}



	public void PlayFadeOut(float fadeTime/*, finishFunc func = null*/)
    {//화면 어두워지기 

        //fadeDelegate = func;
        if (isPlaying)
        {
            return;
        }
        startAlpha = 0f;
        endAlpha = 1f;

        fadeImage.gameObject.SetActive(true);
        fadeImage.gameObject.transform.SetAsLastSibling();

        StartCoroutine(FadeOut(fadeTime));
    }

    IEnumerator DelayFade(bool isFadeIn, float delayTime, float fadeTime)
    {
        
        yield return new WaitForSeconds(delayTime);

        if (isFadeIn)
        {
            PlayFadeIn(fadeTime);
        }
        else 
        {
            PlayFadeOut(fadeTime);
        }
    }


    IEnumerator FadeIn(float fadeTime)
    {//FadeIn = 화면 밝아지는거
        //알파값 1 -> 0
        isPlaying = true;

        Color color = fadeImage.color;
        
        color.a = Mathf.Lerp(startAlpha, endAlpha, fadeInCurTime);

        //시간자체를 나눠서 시간에 맞춰 알파값이 1되도록
        while (color.a > 0f)
        {
            //시간동안 재생될 수 있도록
            fadeInCurTime += Time.deltaTime / fadeTime;

            // 알파 값 계산.  
            color.a = Mathf.Lerp(startAlpha, endAlpha, fadeInCurTime); 
            // 계산한 알파 값 다시 설정.  
            fadeImage.color = color;

            yield return null;
        }

        isPlaying = false;

        fadeInCurTime = 0f;

        if (showUiDel != null)
        {
            showUiDel(true);
            showUiDel = null;
        }
        fadeImage.gameObject.transform.SetAsLastSibling();
        //fadeImage.gameObject.SetActive(false);
    }


    IEnumerator FadeOut(float fadeTime)
    {

        isPlaying = true;

        Color color = fadeImage.color;

        color.a = Mathf.Lerp(startAlpha, endAlpha, fadeOutCurTime);

        //시간자체를 나눠서 시간에 맞춰 알파값이 1되도록
        while (color.a < 1f)
        {
            //시간동안 재생될 수 있도록
            fadeOutCurTime += Time.deltaTime / fadeTime;

            // 알파 값 계산.  
            color.a = Mathf.Lerp(startAlpha, endAlpha, fadeOutCurTime);
            // 계산한 알파 값 다시 설정.  
            fadeImage.color = color;

            yield return null;
        }

        isPlaying = false;
        fadeOutCurTime = 0f;

        if (showUiDel != null)
        {
            showUiDel(false);
            showUiDel = null;
        }
        fadeImage.gameObject.transform.SetAsLastSibling();
        //fadeImage.gameObject.SetActive(false);
    }

    //public IEnumerator StageEndStartFadeOut(float fadeTime)
    //{
    //    startAlpha = 0f;
    //    endAlpha = 1f;

    //    fadeImage.gameObject.SetActive(true);
    //    fadeImage.gameObject.transform.SetAsLastSibling();

    //    isPlaying = true;

    //    Color color = fadeImage.color;

    //    color.a = Mathf.Lerp(startAlpha, endAlpha, fadeOutCurTime);

    //    //시간자체를 나눠서 시간에 맞춰 알파값이 1되도록
    //    while (color.a < 1f)
    //    {
    //        //시간동안 재생될 수 있도록
    //        fadeOutCurTime += Time.deltaTime / fadeTime;

    //        // 알파 값 계산.  
    //        color.a = Mathf.Lerp(startAlpha, endAlpha, fadeOutCurTime);
    //        // 계산한 알파 값 다시 설정.  
    //        fadeImage.color = color;

    //        yield return null;
    //    }

    //    isPlaying = false;
    //    fadeOutCurTime = 0f;

    //    //GameManager.instance.SceneChange(Enums.SceneNum.Result);
    //    UiManager.instance.ShowUI(false);
    //}



    public IEnumerator StageEndFadeOut(float fadeTime)
    {
        startAlpha = 0f;
        endAlpha = 1f;

        fadeImage.gameObject.SetActive(true);
        fadeImage.gameObject.transform.SetAsLastSibling();
        
        isPlaying = true;

        Color color = fadeImage.color;

        color.a = Mathf.Lerp(startAlpha, endAlpha, fadeOutCurTime);

        //시간자체를 나눠서 시간에 맞춰 알파값이 1되도록
        while (color.a < 1f)
        {
            //시간동안 재생될 수 있도록
            fadeOutCurTime += Time.deltaTime / fadeTime;

            // 알파 값 계산.  
            color.a = Mathf.Lerp(startAlpha, endAlpha, fadeOutCurTime);
            // 계산한 알파 값 다시 설정.  
            fadeImage.color = color;

            yield return null;
        }

        isPlaying = false;
        fadeOutCurTime = 0f;

        GameManager.instance.SceneChange(Enums.SceneNum.Result);
    }


    private void Awake()
	{
        if (!instance)
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
               
    }

	// Start is called before the first frame update
	void Start()
    {
        SceneManager.sceneLoaded += this.LoadedsceneEvent;

        //fadeImage = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Fade").gameObject.GetComponent<Image>();

        //if (fadeImage)
        //{
        //    fadeImage.gameObject.SetActive(false);
        //}
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        if (canvas)
        { 
            fadeImage = canvas.transform.Find("Fade").gameObject.GetComponent<Image>(); 
        }

//        fadeImage = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Fade").gameObject.GetComponent<Image>();

        if (fadeImage)
        {
            fadeImage.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= this.LoadedsceneEvent;
    }
}
