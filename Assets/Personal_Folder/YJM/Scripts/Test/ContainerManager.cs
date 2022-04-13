using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public Container container, container2;

    void Start()
    {
        //컨테이너 두개 모두 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            container2.cList.Add(container.cList[0]);
            container.cList.RemoveAt(0);
            //덱에서 손패로 1장 가져오기
            //덱에서 가져온 카드 제거

        }
    }
}
