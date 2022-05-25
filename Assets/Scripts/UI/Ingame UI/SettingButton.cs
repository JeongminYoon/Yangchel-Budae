using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GameManager.instance.SetMenuActive(true); });
        // 설정창 자체에 DontDestroyOnLoad 걸어줘서 쓸려고 했는데 안넘어옴. UI라서 그런가?
        //ㄴ 캔버스 자체에 걸어줘야함.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
