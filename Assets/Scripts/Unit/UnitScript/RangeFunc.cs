﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFunc : Units
{
    public GameObject bulletPrefab;
    //public GameObject muzzle = null;


    public void MuzzleToTarget()
    { 
        


    }

	public override bool Attack(GameObject _target)
	{
        //피격판정은 콜리더로 하기
        if (base.Attack(_target)) //실제 Unit쪽에서 공격 성공하고 나서 총알 생성
        {
            //애니메이션 넣으면 총구 방향 적쪽으로 맞춰 돌리는거
            //45도 돌려주면 댐
            //유닛 자체도 돌리기도 필요할듯 

            //일단 급하게 이렇게 돌리고
            //차후 머즐과 캐릭터 forward dir 외적값 구해서 lerp로 스르륵
            
            if (isLookTarget)
            {
                gameObject.transform.Rotate(new Vector3(0f, 45f, 0f));
            }

            isLookTarget = false;


            if (animController.GetCurrentAnimatorStateInfo(0).IsName("Range_Attack_01"))
            {
                animController.Play("Range_Attack_01",-1,0f);
            }
            else 
            {
                animController.SetTrigger("tAttack");
            }
            
            weaponScript.targetObj = _target;

            //Vector3     muzzlePos = muzzle.transform.position;
            //Quaternion  weaponRot = weapon.transform.rotation;

            //GameObject  bullet = Instantiate(bulletPrefab, muzzlePos, weaponRot);
            //bullet.GetComponent<UnitBullet>().dmg = (int)unitStatus.dmg;

            return true;
        }

        return false;
	}

    public void Fire()
	{
        if (weapon != null)
        {
            weaponScript.Fire();
        }
	}

    public void LookState(int lookState)
    {
        isLookTarget = Funcs.I2B(lookState);
    }
    

    protected override void Awake()
	{
		base.Awake();

        

	}

	// Start is called before the first frame update
	protected override void Start()
    {
        base.Start();

        if (weapon != null)
        {
            weaponScript.FindMuzzle();         
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