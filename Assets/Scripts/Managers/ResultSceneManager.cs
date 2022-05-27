using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultSceneManager : MonoBehaviour
{
    public static ResultSceneManager instance = null;

    float clearTime;
    public TextMeshProUGUI resultText;
    public Image titleLabel;
    public Sprite[] labelSprites = new Sprite[2];
    public GameObject[] Stars = new GameObject[3];

    public void GotoCardSelectScene()
    {
        GameManager.instance.isGameEnd = false;
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

        Funcs.GameManagerHasNotExist();

        if (resultText != null)
        {
             string resultStr = "";
            if (GameManager.instance.isGameWin)
            {
                resultStr = "승리!";
                titleLabel.sprite = labelSprites[0];
            }
            else 
            {
                resultStr = "패배";
                titleLabel.sprite = labelSprites[1];
            }

            //bool temp = GameManager.instance.isGameWin;
            //temp == true ? (resultStr = "WIN" ): (resultStr = "DEFEAT");
            //대입, 호출, 증가, 감소 및 새 개체 식만 문으로 사용가능

            resultText.text = resultStr;
        }
        clearTime = GameManager.instance.inGamePlayTime;
        GameManager.instance.ResetInGameTimer();
        ClearScoreCheck();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearScoreCheck()
    {
        if (clearTime >= 120f)
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
        }
        else if (clearTime >= 90f)
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(false);
        }
        else
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
        }

    }    
}
