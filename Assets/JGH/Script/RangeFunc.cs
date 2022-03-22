using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFunc : Units
{

    public GameObject bulletPrefab;

    
	protected override bool Attack(GameObject _target)
	{
        //피격판정은 콜리더로 하기
        if (base.Attack(_target))
        {
            Transform   gunTr = transform.GetChild(0).transform;
            Vector3     gunPos = gunTr.position;
            Quaternion  gunRot = gunTr.rotation;
            GameObject  bullet = Instantiate(bulletPrefab, gunPos, gunRot);
            bullet.GetComponent<Bullet>().dmg = (int)unitStatus.dmg;

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
