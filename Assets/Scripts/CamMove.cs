using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    // Start is called before the first frame update

    public void Desc(float _productTime, float _moveSpd, float _rotateSpd)
    {
        productTime = _productTime;
        moveSpd = _moveSpd;
        rotateSpd = _rotateSpd;
 
    }

    public GameObject targetObj = null;

    float curTime = 0f;

    float productTime;
    float moveSpd;
    float rotateSpd;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		curTime += Time.deltaTime / 5f;

        transform.position = Vector3.Lerp(this.gameObject.transform.position, targetObj.transform.position, moveSpd * Time.deltaTime);

		if (curTime < 1)
		{


		}
		else
		{
			transform.Rotate(targetObj.transform.up, rotateSpd * Time.deltaTime);
		}
	}
}
