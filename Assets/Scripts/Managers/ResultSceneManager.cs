using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    public static ResultSceneManager instance = null;


    public Text resultText;

    public void GotoCardSelectScene()
    {
        GameManager.instance.SceneChange(Enums.SceneNum.CardSelect);

    }

    public void GameQuit()
    {
        GameManager.instance.GameQuit();
    }




	private void Awake()
	{
        if (instance == null)
        {
            instance = this;
        }


	}

	// Start is called before the first frame update
	void Start()
    {
        if (GameManager.instance == null)
        {
            GameManager tempGameMgr = new GameManager();
            GameObject tempObj = new GameObject();
            tempObj.AddComponent<GameManager>();
        }

        if (resultText != null)
        {
             string resultStr = "";
            if (GameManager.instance.isGameWin)
            {
                resultStr = "WIN";
            }
            else 
            {
                resultStr = "DEFEAT";
            }

            //bool temp = GameManager.instance.isGameWin;
            //temp == true ? (resultStr = "WIN" ): (resultStr = "DEFEAT");
            //대입, 호출, 증가, 감소 및 새 개체 식만 문으로 사용가능

            resultText.text = resultStr;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
