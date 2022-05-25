using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    #region singletone
    /// <singletone>    
    static public TimeScaler instance = null;
    /// <singletone>

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    bool isAction = false;
    public float slowdownFactor = 0.03f;
    public float slowdownLength = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAction == true)
        {
            Time.timeScale = 0.25f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }

    public void DoSlowMotion(float time)
    {
        StartCoroutine(DoAction(time));
    }

    public IEnumerator DoAction(float time = 1f)
    {
        isAction = true;
        yield return new WaitForSecondsRealtime(time);
        isAction = false;
    }
}
