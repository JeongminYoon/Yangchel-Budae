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
    int rotateDir;


    bool isRotate = false;

    public void ZoomIn()
    {
        curTime += Time.deltaTime / (productTime / 3f);

        transform.position = Vector3.Lerp(this.gameObject.transform.position, targetObj.transform.position, moveSpd * Time.deltaTime);
    }

    public void OrbitRotate()
    {
        switch (rotateDir)
        {
            case 0:
                {//반시계방향 회전
                    transform.Rotate(-targetObj.transform.up, rotateSpd * Time.deltaTime);
                }
                break;

            case 1:
                {//시계방향 회전
                    transform.Rotate(targetObj.transform.up, rotateSpd * Time.deltaTime);
                }
                break;
        }
        
    }

    public void RotateStart()
    {
        isRotate = true;
    }

    void Start()
    {
        rotateDir = Random.Range(0, 2);
        
    }

    // Update is called once per frame
    void Update()
    {

		if (curTime < 1)
		{
            ZoomIn();
		}
		else 
		{
            Invoke("RotateStart", 1f);
		}

        if (isRotate)
        {
            OrbitRotate();
        }
	}
}
