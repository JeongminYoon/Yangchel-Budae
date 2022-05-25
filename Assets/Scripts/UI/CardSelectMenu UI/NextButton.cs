using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    Image buttonImage;
    public Sprite[] buttonSprite = new Sprite[2];
    // Start is called before the first frame update

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }
    void Start()
    {
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
