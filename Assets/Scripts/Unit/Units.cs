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
    //c# Ŭ������ �ݹ��̷��۷� ������ ��  + �� �Ҵ�
    //����ü�� �ݹ��̹����  + ���� �Ҵ礤


    void Start()
    {

        
        

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
