using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager2 : MonoBehaviour
{
    public static TestManager2 instance = null;


    int b = 5;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //Debug.Log("TestManager2 Awake 호출");

        Debug.Log(b);
    }

    // Start is called before the first frame update
    void Start()
    {
        TestManager temp = TestManager.instance;
        b = TestManager.instance.a;
        //Debug.Log("TestManager2 Start 호출");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(b);
        //Debug.Log((TestManager.instance).GetHashCode());

        Debug.Log(TestManager.instance.List1.Count);
    }
}
