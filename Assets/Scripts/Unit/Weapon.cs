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
   //public List<UnitBullet> bulletList = new List<UnitBullet>();
    public GameObject muzzle = null;
    
    public GameObject[] muzzleFlashPrefab = new GameObject[9] ; //여러개 만들어 줘야함.
    //private GameObject muzzleFlash = null;
    Vector3 muzzleFlashScale = new Vector3(0.01f, 0.01f, 0.01f);

    public AudioClip fireAC;

    public void Fire(Quaternion ObjRot)
    { //원거리 무기일때 총알 소환 할 함수
      //=> 애니메이션 이벤트에서 관리
        Vector3 muzzlePos = muzzle.transform.position;
        //Quaternion weaponRot = transform.rotation;

        GameObject bullet = Instantiate(bulletPrefab, muzzlePos, ObjRot);

        UnitBullet bulletScript = bullet.GetComponent<UnitBullet>();
        //bulletList.Add(bulletScript);

        //Instantiate(muzzleFirePrefab, muzzle.transform);
        InstantiateMuzzleFlash();

        if (bulletScript != null)
        {
            bulletScript.dmg = dmg;
            bulletScript.targetObj = targetObj;
        }

        AudioManager.instance.unitAus.PlayOneShot(fireAC);

    }

    public void Fire()
    { //원거리 무기일때 총알 소환 할 함수
      //=> 애니메이션 이벤트에서 관리
        Vector3 muzzlePos = muzzle.transform.position;
        Quaternion weaponRot = transform.rotation;

        GameObject bullet = Instantiate(bulletPrefab, muzzlePos, weaponRot);
        UnitBullet bulletScript = bullet.GetComponent<UnitBullet>();
        //bulletList.Add(bulletScript);
        InstantiateMuzzleFlash();



        if (bulletScript != null)
        {
            bulletScript.dmg = dmg;
            bulletScript.targetObj = targetObj;
        }

        AudioManager.instance.unitAus.PlayOneShot(fireAC);
    }

    public void WeaponColState(int colState)
    { //근거리 무기 콜라이더 껏다 켰다할 함수
      //=> 애니메이션 이벤트에서 MeleeFunc Slash함수 호출해서 관리
        GetComponent<BoxCollider>().enabled = Funcs.I2B(colState);

        if (Funcs.I2B(colState))
        { AudioManager.instance.unitAus.PlayOneShot(fireAC); }
        
    }

    public void FindMuzzle()
    {//RangeFunc쪽에서 호출해주기 
        if (muzzle == null)
        {
            muzzle = Funcs.FindGameObjectInChildrenByName(this.gameObject, "Muzzle");
        }
    }

    public void DeadTargetException(GameObject isDeadTarget)
    {
        if (targetObj == isDeadTarget)
        {
            targetObj = null;
        }

        //foreach (UnitBullet _bullet in bulletList)
        //{
        //    if (_bullet.targetObj = isDeadTarget)
        //    {
        //        _bullet.targetObj = null;
        //    }
        //}
    }

    public void InstantiateMuzzleFlash()
    {
        int rand = Random.Range(0, 9);

        GameObject flash = Instantiate(muzzleFlashPrefab[rand], muzzle.transform);
        flash.GetComponent<MuzzleFlashFX>().maxScale = muzzleFlashScale;
        flash.transform.localScale = muzzleFlashScale;


        //if (muzzleFlash)
        //{
        //    Destroy(muzzleFlash);
        //}

        //muzzleFlash = flash;
    }
    //public void BulletDestory()
    //{
    //    //이렇게 되면 유닛이 먼저 뒤져버렸을 때 문제 생김. 
    //    //총알은 유닛이 죽던말던 그냥 알아서 할일 하면 됨. 필요없을듯.
    //    //대신 타겟이 죽었을 때만 예외처리.
    //    foreach (UnitBullet _bullet in bulletList)
    //    {
    //        if (_bullet.isDead)
    //        {
    //            UnitBullet temp = _bullet;
    //            bulletList.Remove(_bullet);
    //            Destroy(temp.gameObject);
    //        }
    //    }
    //}


    // Start is called before the first frame update
    void Start()
    {
        if (name.Contains("Range") || name.Contains("Tower"))
        {
            muzzleFlashScale = new Vector3(0.025f, 0.025f, 0.025f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //DeadTargetException();

        
    }

    public void Release()
    {
        targetObj = null;

        if (muzzle)
        {
            muzzle = null;
        }

        //if (muzzleFlash)
        //{
        //    Destroy(muzzleFlash);
        //}

        //bulletList.Clear();
    }

	private void OnTriggerEnter(Collider other)
	{

        //근접 애들만 사용할거 

        if (targetObj != null)
        {
            if (other.gameObject == targetObj)
            {
                Units temp = other.gameObject.GetComponent<Units>();

                if (temp != null)
                {
                    temp.Hit(dmg,transform.position);
                    GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
	}

}
