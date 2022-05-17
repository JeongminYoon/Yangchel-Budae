using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFunc : Units
{

	public AudioClip[] meleeAC = new AudioClip[(int)Enums.eUnitFXS.End];
	public override bool Attack(GameObject _target)
	{
		if (base.Attack(_target))
		{
			animController.SetTrigger("tAttack");
            weaponScript.targetObj = _target;
            return true;
        }
        return false;
    }

	public override void LoadSoundClips()
	{
		base.LoadSoundClips();

		unitAC[(int)Enums.eUnitFXS.AttackFXS] = Resources.Load("Sounds/Unit/Melee_Fire") as AudioClip;

		if (weaponScript != null)
		{
			weaponScript.fireAC = unitAC[(int)Enums.eUnitFXS.AttackFXS];
		}
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

		//unitAC[(int)Enums.eUnitFXS.HitFXS] = Resources.Load("Sounds/Unit/Unit_Hit_0") as AudioClip;
	}

    // Start is called before the first frame update
     protected override void Start()
     {
        base.Start();

		LoadSoundClips();
     }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

		#region 임시 FSM
		//임시 FSM => 걍 좆도 FSM 아님 ㅋㅋ 
		//if (curState != Enums.UnitState.Attack)
		//{
		//	if (base.Attack(targetObj))
		//	{
		//		SetState(Enums.UnitState.Attack);
		//	}
		//	else
		//	{
		//		SetState(Enums.UnitState.Walk);
		//	}
		//}

		//switch (curState)
		//{
		//    case Enums.UnitState.Walk:
		//        {
		//            Walk();
		//        }
		//    break;

		//    case Enums.UnitState.Attack:
		//        {
		//            Attack(targetObj);
		//        }
		//    break;

		//}
		#endregion

		Walk();
		Attack(targetObj);

	}

    private void OnCollisionEnter(Collision collision)
    {

		if (collision.gameObject.CompareTag("Tower") || collision.gameObject.CompareTag("Nexus"))
		{
			if (targetObj.CompareTag("Tower") || targetObj.CompareTag("Nexus"))
			{
				Units unitScript = collision.gameObject.GetComponent<Units>();

				if (unitScript != null)
				{
					if (unitScript.IsEnemy != isEnemy)
					{
						if (!unitStatus.isDead)
						{ navAgent.isStopped = true; }
						//SetState(Enums.UnitState.Attack);
					}
				}
			}
		}
	}

}
