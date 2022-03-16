using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public static Units instance = null;


    ScriptableObject_Test temp = null;
    // Start is called before the first frame update


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        int c = 10;
        int d = 20;

    }
    //c# 클래스는 콜바이레퍼로 무조건 됨  + 힙 할당
    //구조체는 콜바이밸류로  + 스택 할당ㄴ


    void Start()
    {

        
        

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
