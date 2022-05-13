using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashFX : MonoBehaviour
{
    public Vector3 maxScale;

    float aliveTime = 0.2f;
    float curTime = 0f;

	void Awake()
	{
		
	}

	// Start is called before the first frame update
	void Start()
    {
    


    }

    // Update is called once per frame
    void Update()
    {

		//lerp�� ���ϴ� �ð����� �� ��ȭ�� ���� ��� 
		//�䷸�� ����ϸ��.
		//
		curTime += Time.deltaTime / aliveTime;

		if (curTime < 1)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, curTime);
		}
		else
		{
			Destroy(this.gameObject);
		}


		//transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 10f);
	}
}
