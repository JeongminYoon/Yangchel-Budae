using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTest : MonoBehaviour
{
    public static SceneChangeTest instance = null;


    public void Awake()
	{
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        Debug.Log("SceneChanger Awake ȣ��");
        
        curScene = (SceneNum)SceneManager.GetActiveScene().buildIndex;
	}

	public enum SceneNum
	{
       Scene1,
       Scene2,
       Scene3,
       SceneEnd
	}

    public SceneNum curScene = SceneNum.SceneEnd;
    public SceneNum pastScene = SceneNum.SceneEnd;
    public SceneNum nextScene = SceneNum.SceneEnd;

    public void SceneChange(SceneNum sceneNum)
    {
        Debug.Log(sceneNum + "���� �� �ٲٶ�� ���");
        nextScene = sceneNum;

        if (curScene != nextScene && nextScene != SceneNum.SceneEnd)
        {
            pastScene = curScene;
            curScene = sceneNum;
            nextScene = SceneNum.SceneEnd;
            Debug.Log(sceneNum + "���� �� �ٲٱ� ����");
            SceneManager.LoadScene((int)sceneNum);
            
        }
        else
        {
            Debug.Log(sceneNum + "���� �� �ٲٱ� ����");
            return;
        }
    }

    public SceneNum CheckCurScene()
    {
         return curScene;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SceneChanger Start ȣ��");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneChange(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneChange((SceneNum)1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneChange((SceneNum)2);
        }
    }
}
