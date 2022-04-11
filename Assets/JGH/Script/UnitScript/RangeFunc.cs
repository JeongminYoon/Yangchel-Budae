using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFunc : Units
{
    public GameObject bulletPrefab;
    public GameObject muzzle;
    
	public override bool Attack(GameObject _target)
	{
        //피격판정은 콜리더로 하기
        if (base.Attack(_target)) //실제 Unit쪽에서 공격 성공하고 나서 총알 생성
        {
            //애니메이션 넣으면 총구 방향 적쪽으로 맞춰서
            //유닛 자체도 돌리기도 필요할듯 

            Vector3     muzzlePos = muzzle.transform.position;
            Quaternion  weaponRot = weapon.transform.rotation;

            GameObject  bullet = Instantiate(bulletPrefab, muzzlePos, weaponRot);
            bullet.GetComponent<UnitBullet>().dmg = (int)unitStatus.dmg;

            return true;
        }

        return false;
	}

    

    protected override void Awake()
	{
		base.Awake();

        

	}

	// Start is called before the first frame update
	protected override void Start()
    {
        base.Start();

        if (weapon != null && muzzle == null)
        {
            muzzle = Funcs.FindGameObjectInChildrenByName(weapon, "Muzzle");
        }
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        Walk();
        if (targetObj != null)
        { Attack(targetObj); }
                
    }
}
