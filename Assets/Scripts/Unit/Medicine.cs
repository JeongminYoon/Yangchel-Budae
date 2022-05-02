﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public int healAmount;
    //public Vector3 rotateAmount;
    public Vector3 rotate;

    public GameObject medic;
    public GameObject targetObj;

    private Vector3 targetCenterPos;
    public float curAliveTime = 0f;


	public void Move()
	{
        //아군 타겟 유닛이 죽어서 Destory될 경우 예외 처리 해줘야함.

        if (targetObj != null)
        {
            targetCenterPos = targetObj.GetComponent<Units>().center.transform.position;
        }
        else
        {
            targetCenterPos.y = -1;
        }

        Vector3 dir = targetCenterPos - this.gameObject.transform.position;
        transform.position += dir.normalized * 5f * Time.deltaTime;
    }

    

	// Start is called before the first frame update
	void Start()
    {
        rotate = new Vector3();
       // rotateAmount = new Vector3(5, 5, 5);
    }

    // Update is called once per frame
    void Update()
    {

        if (targetObj != null && targetObj.GetComponent<Units>().unitStatus.isDead)
        {
            targetObj = null;
        }

        if (medic != null &&  medic.GetComponent<Units>().unitStatus.isDead)
        {
            medic = null;
        }


        Move();
        rotate += new Vector3(5, 0, 0);
        
        curAliveTime += Time.deltaTime;

        if (curAliveTime >= 3f)
        {
            Release();
            Destroy(this.gameObject);
        }
        
        //transform.rotation = Quaternion.Euler(rotate);

        //계속 타겟 Obj 위치 판단해서 움직이는걸루다가?
        
        
    }

    public void Release()
    {
        if (medic)
        { medic = null; }

        if (targetObj)
        {
            targetObj = null;
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {

        if (targetObj == other.gameObject)
        {
            Units hitObj = other.gameObject.GetComponent<Units>();
            //hitObj.unitStatus.curHp += healAmount;
            hitObj.Cure(healAmount);

            Release();
            Destroy(this.gameObject);
        }


        if (other.CompareTag("MapData"))
        {
            Release();
            Destroy(this.gameObject);
        }

    }
}
