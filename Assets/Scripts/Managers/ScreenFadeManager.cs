using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public delegate void finishFunc();
    public finishFunc fadeDelegate;

    public Image fadeImage;

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

        if (fadeDelegate != null)
        {
            fadeDelegate();
            fadeDelegate = null;
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

        if (fadeDelegate != null)
        {
            fadeDelegate();
            fadeDelegate = null;
        }
        fadeImage.gameObject.transform.SetAsLastSibling();
        //fadeImage.gameObject.SetActive(false);
    }

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

        fadeImage = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
        fadeImage.gameObject.SetActive(false);
    }

	// Start is called before the first frame update
	void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
