using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{

    TestManager()
    {
        Debug.Log("TestManager 생성자");
    }

    public static TestManager instance = null;

    public int a = 3;

    public List<int> List1 = new List<int>();

    private void Awake()
	{
        if (instance == null)
        {
            Debug.Log("TestManager instance 할당");
            instance = this;
        }
        Debug.Log("TestManager Awake 호출");

        Debug.Log("맨처음 : " + List1.Count);
    }

	// Start is called before the first frame update
	void Start()
    {
        TestManager temp = TestManager.instance;
        Debug.Log("TestManager Start 호출");

        for (int i = 0; i < 10; ++i)
        {
            List1.Add(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("TestManager Update 호출");

        //Debug.Log((instance).GetHashCode());

        Debug.Log(List1.Count);
    }
}
