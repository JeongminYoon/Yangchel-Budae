using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFunc : Units
{
    public GameObject bulletPrefab;
    //public GameObject muzzle = null;
    
	public override bool Attack(GameObject _target)
	{
        //�ǰ������� �ݸ����� �ϱ�
        if (base.Attack(_target)) //���� Unit�ʿ��� ���� �����ϰ� ���� �Ѿ� ����
        {
            //�ִϸ��̼� ������ �ѱ� ���� �������� ���� �����°�
            //���� ��ü�� �����⵵ �ʿ��ҵ� 

            //Vector3     muzzlePos = muzzle.transform.position;
            //Quaternion  weaponRot = weapon.transform.rotation;

            //GameObject  bullet = Instantiate(bulletPrefab, muzzlePos, weaponRot);
            //bullet.GetComponent<UnitBullet>().dmg = (int)unitStatus.dmg;

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
