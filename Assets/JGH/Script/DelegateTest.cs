using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTest : MonoBehaviour
{
    //c# 델리게이트, 이벤트 테스트

    //C# 델리게이트 => C++의 함수 포인터와 비슷한듯?

    delegate int MyDelegate(int a);

    static int TestFunc(int a)
    {
        return a;
    }

    static void RunDelegate(MyDelegate func)
    {
        int a = 10;
        int b = func(10);

        Debug.Log(a);
    }

    MyDelegate testDele = new MyDelegate(TestFunc);

    
    

}
