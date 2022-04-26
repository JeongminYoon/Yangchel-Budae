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
            //����, ȣ��, ����, ���� �� �� ��ü �ĸ� ������ ��밡��

            resultText.text = resultStr;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
