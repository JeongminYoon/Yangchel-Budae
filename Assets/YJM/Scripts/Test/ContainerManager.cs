using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public Container container, container2;

    void Start()
    {
        //�����̳� �ΰ� ��� �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            container2.cList.Add(container.cList[0]);
            container.cList.RemoveAt(0);
            //������ ���з� 1�� ��������
            //������ ������ ī�� ����

        }
    }
}
