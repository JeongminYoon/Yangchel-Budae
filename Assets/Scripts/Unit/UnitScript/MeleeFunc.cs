using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFunc : Units
{
	public override bool Attack(GameObject _target)
	{
        //if (weapon != null)
        //{
        //    weapon.GetComponent<BoxCollider>().enabled = base.Attack(_target);
        //    if (weapon.GetComponent<BoxCollider>().enabled == true)
        //    {
        //        animController.SetBool("bWalk", false);
        //        animController.SetTrigger("tAttack");
        //        return true;
        //    }
        //}

        if (base.Attack(_target))
        {
            animController.SetTrigger("tAttack");
            weaponScript.targetObj = _target;
            return true;
        }


		return false;
    }

    public void Slash(int colState)
    {//애니메이션 동작에 맞춰서 이거 틀어줄꺼임.
        if (weapon != null)
        {
            weaponScript.WeaponColState(colState);
        }
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

        //if (targetObj != null)
        //{ targetDist = Vector3.Magnitude(this.gameObject.transform.position - targetObj.transform.position); }

        Attack(targetObj);


    }


	private void OnTriggerEnter(Collider other)
	{
        //if (other.CompareTag("Weapon"))
        //{//근접 공격
        // //=> 무기 객체의 주인인 게임오브젝트의
        // //Units 클래스 가져와서
        // //아군인지 적인지 판별.(현재 유닛의 isEnemy와)
        // //공격이 아군이면 데미지 안주고 적군이면 데미지 주기
        // //=> 공격 한놈한테만 들어가게 하기 해야하는디...
        //}
        //else if(other.CompareTag("Bullet"))
        //{//총알 (유닛이던, 타워던)
        // //총알 생성하는 모든 곳에 자기 isEnemy 변수 던져주고
        // //충돌한 놈은 불릿의 isEnemy 변수 판별해서
        // //아군이면 데미지 안주고 적군이면 데미지 주기
        //}
        //Units[] temp = other.transform.GetComponentsInParent<Units>();


        //=> 그냥 근접무기 혹은 총알 쪽에서
        //타겟 오브젝트랑 동일한지 확인 한뒤 처리 해주자!
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    { 
        if (hit.gameObject.CompareTag("Tower") || hit.gameObject.CompareTag("Nexus"))
		{
            if (targetObj.CompareTag("Tower") || targetObj.CompareTag("Nexus"))
            {
                animController.SetTrigger("tAttack");
                weaponScript.targetObj = targetObj;
            }
            
        }
    }
}
