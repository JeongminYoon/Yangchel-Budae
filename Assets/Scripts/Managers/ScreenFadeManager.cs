using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFadeManager : MonoBehaviour
{
    //������ UI�Ŵ���������
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
    //����, ��ȯ���� ������� ���ϳ�...?
    //�ƴϸ� ��¥ �׳� �Լ������͸� �ѱ�ٴ��� �� ����

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
    {//FadeIn = ȭ�� ������°� 
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
    {//ȭ�� ��ο����� 

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
    {//FadeIn = ȭ�� ������°�
        //���İ� 1 -> 0
        isPlaying = true;

        Color color = fadeImage.color;
        
        color.a = Mathf.Lerp(startAlpha, endAlpha, fadeInCurTime);

        //�ð���ü�� ������ �ð��� ���� ���İ��� 1�ǵ���
        while (color.a > 0f)
        {
            //�ð����� ����� �� �ֵ���
            fadeInCurTime += Time.deltaTime / fadeTime;

            // ���� �� ���.  
            color.a = Mathf.Lerp(startAlpha, endAlpha, fadeInCurTime); 
            // ����� ���� �� �ٽ� ����.  
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

        //�ð���ü�� ������ �ð��� ���� ���İ��� 1�ǵ���
        while (color.a < 1f)
        {
            //�ð����� ����� �� �ֵ���
            fadeOutCurTime += Time.deltaTime / fadeTime;

            // ���� �� ���.  
            color.a = Mathf.Lerp(startAlpha, endAlpha, fadeOutCurTime);
            // ����� ���� �� �ٽ� ����.  
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

    //    //�ð���ü�� ������ �ð��� ���� ���İ��� 1�ǵ���
    //    while (color.a < 1f)
    //    {
    //        //�ð����� ����� �� �ֵ���
    //        fadeOutCurTime += Time.deltaTime / fadeTime;

    //        // ���� �� ���.  
    //        color.a = Mathf.Lerp(startAlpha, endAlpha, fadeOutCurTime);
    //        // ����� ���� �� �ٽ� ����.  
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

        //�ð���ü�� ������ �ð��� ���� ���İ��� 1�ǵ���
        while (color.a < 1f)
        {
            //�ð����� ����� �� �ֵ���
            fadeOutCurTime += Time.deltaTime / fadeTime;

            // ���� �� ���.  
            color.a = Mathf.Lerp(startAlpha, endAlpha, fadeOutCurTime);
            // ����� ���� �� �ٽ� ����.  
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
