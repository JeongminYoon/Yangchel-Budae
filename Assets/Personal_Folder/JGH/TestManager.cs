using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{

    TestManager()
    {
        Debug.Log("TestManager ������");
    }

    public static TestManager instance = null;

    public int a = 3;

    public List<int> List1 = new List<int>();

    private void Awake()
	{
        if (instance == null)
        {
            Debug.Log("TestManager instance �Ҵ�");
            instance = this;
        }
        Debug.Log("TestManager Awake ȣ��");

        Debug.Log("��ó�� : " + List1.Count);
    }

	// Start is called before the first frame update
	void Start()
    {
        TestManager temp = TestManager.instance;
        Debug.Log("TestManager Start ȣ��");

        for (int i = 0; i < 10; ++i)
        {
            List1.Add(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("TestManager Update ȣ��");

        //Debug.Log((instance).GetHashCode());

        Debug.Log(List1.Count);
    }
}
