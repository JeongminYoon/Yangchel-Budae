using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTest : MonoBehaviour
{
    //c# 델리게이트, 이벤트 테스트

    //C# 델리게이트 => C++의 함수 포인터와 비슷한듯?
    //요걸로 이벤트 만들어서 오브젝트 죽었을 때 처리 ㄱㄱㄱㄱ

	//콜백함수들과 반환형, 인자 같게 만들어야함.
    public delegate bool TestHandler(GameObject unit);

	bool DeathDelegate(GameObject unit)
	{
		return false;
	}

	TestHandler handler;

	private void Start()
	{
		handler = new TestHandler(DeathDelegate);
		handler += CallBack1;
		handler += CallBack2;
		handler += CallBack3;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TestRealFunc(handler);
		}
	}

	public void TestRealFunc(TestHandler handler)
	{
		Debug.Log("Death");


		handler(this.gameObject);
	}

	public bool CallBack1(GameObject unit)
	{
		Debug.Log("콜백함수1 실행");
		return false;
	}
	public bool CallBack2(GameObject unit)
	{
		Debug.Log("콜백함수2 실행");
		return false;
	}
	public bool CallBack3(GameObject unit)
	{
		Debug.Log("콜백함수3 실행");
		return false;
	}




}
