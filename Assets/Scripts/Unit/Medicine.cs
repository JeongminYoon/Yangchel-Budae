using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public int healAmount;
    //public Vector3 rotateAmount;
    public Vector3 rotate;

    public GameObject medic;
    public GameObject targetObj;

	public void Move()
	{
        //아군 타겟 유닛이 죽어서 Destory될 경우 예외 처리 해줘야함.
        Vector3 centerPos = targetObj.GetComponent<Units>().center.transform.position;
        Vector3 dir = centerPos - this.gameObject.transform.position;

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
        Move();
        rotate += new Vector3(5, 0, 0);
        
        //transform.rotation = Quaternion.Euler(rotate);

        //계속 타겟 Obj 위치 판단해서 움직이는걸루다가?
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != medic)
        {

            if (targetObj == other.gameObject)
            {
                Units hitObj = other.gameObject.GetComponent<Units>();
                //hitObj.unitStatus.curHp += healAmount;
                hitObj.Cure(healAmount);
                
                Destroy(this.gameObject);
            }
            else if(other.CompareTag("MapData"))
            {
                Destroy(this.gameObject);
            }
        }

	}
}
