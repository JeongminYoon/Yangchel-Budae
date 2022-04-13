using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int dmg;
    public bool isEnemy;
    public GameObject targetObj;

    //for Range Weapon -> 상속으로 할까...?
    public GameObject bulletPrefab;
    public GameObject muzzle = null;

    public void Fire()
    { //원거리 무기일때 총알 소환 할 함수
	  //=> 애니메이션 이벤트에서 관리
		Vector3 muzzlePos = muzzle.transform.position;
		Quaternion weaponRot = transform.rotation;

		GameObject bullet = Instantiate(bulletPrefab, muzzlePos, weaponRot);
		UnitBullet bulletScript = bullet.GetComponent<UnitBullet>();

        if (bulletScript != null)
        {
            bulletScript.dmg = dmg;
            bulletScript.targetObj = targetObj;
        }

	}

    public void WeaponColState(int colState)
    { //근거리 무기 콜라이더 껏다 켰다할 함수
      //=> 애니메이션 이벤트에서 MeleeFunc Slash함수 호출해서 관리
        GetComponent<BoxCollider>().enabled = Funcs.I2B(colState);
    }

    public void FindMuzzle()
    {//RangeFunc쪽에서 호출해주기 
        if (muzzle == null)
        {
            muzzle = Funcs.FindGameObjectInChildrenByName(this.gameObject, "Muzzle");
        }
    }

// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject == targetObj)
        {
            Units temp = other.gameObject.GetComponent<Units>();
            
            if (temp != null)
            {
                temp.Hit(dmg);
            }
        }

	}

}
