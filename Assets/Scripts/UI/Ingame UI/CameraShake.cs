using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region singletone
    /// <singletone>
    static public CameraShake instance = null;
    /// <singletone>

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    // Start is called before the first frame update
    public Camera mainCamera;
    Vector3 originalCamPos;

    [SerializeField] [Range(0.0f, 0.1f)] float shakeRange = 0.05f;
    //[SerializeField] [Range(0.1f, 1f)] float duration = 0.5f;

    float timer = 0f;

    private void Start()
    {
        originalCamPos = mainCamera.transform.position;
        x = originalCamPos.x;
        y = originalCamPos.y;
        InvokeRepeating("RestoringForce", 0f, 0.03f);
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shake(new Vector3(0.5f, 0f, 0), 10f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Shake(new Vector3(0f, 0f, 0.5f), 10f);
        }

    }


    float restoringForce;
    float x;
    float y;
    void RestoringForce()
    {
        restoringForce = Mathf.Abs(transform.position.x / 100);
        //if (transform.position.x >= 0.001f)
        if (transform.position.x > originalCamPos.x)
        {
            x = transform.position.x - restoringForce;
        }
        if (transform.position.x < originalCamPos.x)
        {
            x = transform.position.x + restoringForce;
        }
        if (transform.position.y > originalCamPos.y)
        {
            y = transform.position.y - restoringForce;
        }
        if (transform.position.y < originalCamPos.y)
        {
            y = transform.position.y + restoringForce;
        }
        //else
        //{
        //    transform.position = originalCamPos;
        //}

        transform.position = new Vector3(x, y, originalCamPos.z);
    }

    public void Shake(Vector3 vec, float time)
    {
        iTween.ShakePosition(this.gameObject, vec, time);
    }

    public void Shake(float time, float power = 0.05f, float cycle = 0.05f)  //Èçµé½Ã°£, ÈçµéÈû, Èçµé¸² ÁÖ±â(¸îÃÊ¸¶´Ù ÈçµéÁö)
    {
        IEnumerator shakeCoroutine = ShakeCoroutine(time ,power,cycle);
        StartCoroutine(shakeCoroutine);
    }

    IEnumerator ShakeCoroutine(float time, float power, float cycle)
    {
        for (int i = 0; i <= time / cycle; i++)
        {
            mainCamera.transform.position += Random.insideUnitSphere * power;
            yield return new WaitForSeconds(cycle / 2);
            mainCamera.transform.position = originalCamPos;
            yield return new WaitForSeconds(cycle / 2);
        }
    }

    #region Invoke»ç¿ë ±¸ ÄÚµå
    //void TimerSetting()
    //{
    //    timer -= Time.deltaTime;
    //    if (timer <= 0f)
    //    {
    //        timer = 0f;
    //    }
    //}

    //float timerEffect = 0f;
    //void TimerEffectSetting()
    //{
    //    if (timer > 1)
    //    {
    //        timerEffect = 1;
    //    }
    //    else
    //    {
    //        timerEffect = timer;
    //    }
    //}

    //bool swi = false;

    //public void Shake(float time, float power = 0.05f, float cycle = 0.05f)
    //{
    //    shakeRange = power;
    //    if (swi == false)
    //    {
    //        swi = true;
    //        //originalCamPos = mainCamera.transform.position;
    //        InvokeRepeating("StartShake", 0f, cycle);
    //        timer += time;
    //    }
    //    else
    //    {
    //        StopShake();
    //        InvokeRepeating("StartShake", 0f, cycle);
    //        timer = time;
    //    }
    //}

    //void StartShake()
    //{
    //    float cameraPosx = (Random.value * shakeRange * 2 - shakeRange) * timerEffect;
    //    float cameraPosy = (Random.value * shakeRange * 2 - shakeRange) * timerEffect;
    //    Vector3 ShakecameraPos = mainCamera.transform.position;
    //    ShakecameraPos.x += cameraPosx;
    //    ShakecameraPos.y += cameraPosy;
    //    //if (ShakecameraPos.x > originalCamPos.x + 0.5f)
    //    //{
    //    //    ShakecameraPos.x = originalCamPos.x + 0.2f;
    //    //}
    //    //if (ShakecameraPos.x < originalCamPos.x - 0.5f)
    //    //{
    //    //    ShakecameraPos.x = originalCamPos.x - 0.2f;
    //    //}
    //    //if (ShakecameraPos.y > originalCamPos.y + 0.5f)
    //    //{
    //    //    ShakecameraPos.y = originalCamPos.y + 0.2f;
    //    //}
    //    //if (ShakecameraPos.y < originalCamPos.y - 0.5f)
    //    //{
    //    //    ShakecameraPos.y = originalCamPos.y - 0.2f;
    //    //}
    //    mainCamera.transform.position = ShakecameraPos;
    //}

    //void StopShake()
    //{
    //    CancelInvoke("StartShake");
    //    //mainCamera.transform.position = originalCamPos;
    //}
    #endregion
}
