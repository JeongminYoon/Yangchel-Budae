using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    #region singletone
    static public UiManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        ShowUI(true);
    }
    #endregion

    public GameObject gamePlayUI;

    public void ShowUI(bool i = true)
    {
        if(i == true)
        {
            gamePlayUI.SetActive(true);
        }
        else
        {
            gamePlayUI.SetActive(false);
        }
    }
}
