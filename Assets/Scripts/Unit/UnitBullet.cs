using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBullet : MonoBehaviour
{
    //충돌 메세지를 발생하는것은 리지드바디임으로
    //충돌 판정을 하기 위해선 최소 하나는 리지드바디 가지고 있어야함.      

    //또한 두 게임 오브젝트 콜리더 중 최소 하나가 트리거 콜라이더면 호출됨
    //-> 양쪽 모두에서 일어남.
    //On Trigger ~ 함수로 실행됨. 또한 그대로 통과.
    //또한 Collider형을 받아오는데, 여기서는 상세한 충돌 정보 없음.

    //OnCollision은 일반 충돌 시 발생
    //또한 인자로 Collision형을 받아오는데, 여기서는 상세한 충돌 정보 유

    public int dmg;
    public float bulletSpd = 10f;
    public bool isDead = false;

    public GameObject targetObj;
    public float aliveTime = 0f;
    
    // Start is called before the first frame update

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObj != null && targetObj.GetComponent<Units>().unitStatus.isDead)
        {
            targetObj = null;
        }

        aliveTime += Time.deltaTime;

        if (aliveTime >= 3f)
        {
            Release();
            Destroy(this.gameObject);
            //총알 Destroy 하는건 Weapon쪽에서 해줄예정.
                //=> 근데 이러면 총알 날라가는데 유닛 뒤져버리면 못없애는디 ㅋㅋ; 좆댔넹
        }

        transform.position += transform.forward * bulletSpd * Time.deltaTime;
    }

    public void Release()
    {
        targetObj = null;
        //isDead = true;
    }


	private void OnTriggerEnter(Collider other)
	{
        //if (other.tag == "Enemy" || other.tag == "Tower")
        //{//나중에 타워용 총알 따로 만들어서 나눠야함 일단 이렇게
        //        //=> 지금 이거 땜시 총알 좀 뒤쪽으로 쏘면 지가 쳐맞고 뒤짐 ㅋㅋ;
        //    Debug.Log("ㅇㅇ");

        //    Units temp =  other.gameObject.GetComponent<Units>();

        //    if (temp != null)
        //    {
        //        temp.Hit(dmg);
        //    }

        //    Destroy(this.gameObject);
        //}
        if (targetObj != null)
        {
            if (other.gameObject == targetObj)
            {
                Units temp = other.gameObject.GetComponent<Units>();

                if (temp != null)
                {
                    temp.Hit(dmg,transform.position,-transform.position.normalized);
                    Release();
                    Destroy(this.gameObject);
                }
            }
        }
        
        if (other.gameObject.CompareTag("MapData"))
        {
            Release();
            Destroy(this.gameObject);
        }
	}


	private void OnCollisionEnter(Collision collision)
	{
        Debug.Log("Collision On");
	}
}
