using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //�浹 �޼����� �߻��ϴ°��� ������ٵ�������
    //�浹 ������ �ϱ� ���ؼ� �ּ� �ϳ��� ������ٵ� ������ �־����.      

    //���� �� ���� ������Ʈ �ݸ��� �� �ּ� �ϳ��� Ʈ���� �ݶ��̴��� ȣ���
    //-> ���� ��ο��� �Ͼ.
    //On Trigger ~ �Լ��� �����. ���� �״�� ���.
    //���� Collider���� �޾ƿ��µ�, ���⼭�� ���� �浹 ���� ����.

    //OnCollision�� �Ϲ� �浹 �� �߻�
    //���� ���ڷ� Collision���� �޾ƿ��µ�, ���⼭�� ���� �浹 ���� ��

    public int dmg;

    public float aliveTime = 0f;
    
    // Start is called before the first frame update

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime += Time.deltaTime;

        if (aliveTime >= 3f)
        {
            Destroy(this.gameObject);
        }

        transform.position += transform.forward * 5f * Time.deltaTime;
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Enemy")
        {
           Units temp =  other.gameObject.GetComponent<Units>();

            if (temp != null)
            {
                temp.Hit(dmg);
            }

            Destroy(this.gameObject);
        }
	}
}
