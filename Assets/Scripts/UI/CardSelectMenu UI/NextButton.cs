using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    #region singletone
    static public NextButton instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    Image buttonImage;
    public Sprite[] buttonSprite = new Sprite[2];
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeButtonColor(int num)
    {
        buttonImage.sprite = buttonSprite[num];
    }
}
