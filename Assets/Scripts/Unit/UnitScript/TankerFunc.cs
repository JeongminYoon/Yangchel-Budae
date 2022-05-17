using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankerFunc : Units
{//탱커는 유닛 공격 안하고 기냥 무적권 타워 향해서 돌격 해야함.

	public AudioClip[] tankerAC = new AudioClip[(int)Enums.eUnitFXS.End];

	public override void LoadSoundClips()
	{
		base.LoadSoundClips();

		unitAC[(int)Enums.eUnitFXS.AttackFXS] = Resources.Load("Sounds/Unit/Melee_Fire") as AudioClip;

		if (weaponScript != null)
		{
			weaponScript.fireAC = unitAC[(int)Enums.eUnitFXS.AttackFXS];
		}
	}
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
	
	public void Slash(int colState)
	{
		if (weapon != null && targetObj != null)
		{
			transform.LookAt(targetObj.transform);
			weaponScript.WeaponColState(colState);
		}
	}

	public override void Walk()
	{
		if (unitStatus.isDead || GameManager.instance.isGameEnd)
		{
			return;
		}

		if (targetObj != null)
		{
			CalcToObj(targetObj);

			if (targetDist > unitStatus.atkRange)
			{
				//transform.position += targetDir * unitStatus.moveSpd * Time.deltaTime;
				if (!unitStatus.isDead)
				{ 
					navAgent.isStopped = false;
					animController.SetBool("bWalk", true);
				}

				//transform.LookAt(targetObj.transform);
			}
			else
			{
				if (!unitStatus.isDead)
				{ 
					navAgent.isStopped = true;
					animController.SetBool("bWalk", false);
				}
			}
		}
		else
		{
			SearchTower();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		//tankerAC[(int)Enums.eUnitFXS.HitFXS] = Resources.Load("Sounds/Unit/Unit_Hit_0") as AudioClip;
		//unitAC[(int)Enums.eUnitFXS.HitFXS] = Resources.Load("Sounds/Unit/Unit_Hit_0") as AudioClip;
	}

	protected override void Start()
	{

		DeathEventSetting();

		ScriptableObj_DeepCopy(); //깊은 복사

		WeaponSetting();
		CenterSetting();

		//SettingAus();

		animController = this.gameObject.GetComponent<Animator>();
		//charContoller = this.gameObject.GetComponent<CharacterController>();
		navAgent = this.gameObject.GetComponent<NavMeshAgent>();
		navAgent.speed = unitStatus.moveSpd;

		LoadSoundClips();

		SearchTower();

	}

	protected override void Update()
	{
		//base.Update();

		if (targetObj == null)
		{
			searchCurTime += Time.deltaTime;

			if (searchCurTime >= searchTime)
			{

				SearchTower();
				searchCurTime = 0f;
			}
		}

		//if (targetObj != null)
		//{
		//	transform.LookAt(targetObj.transform);
		//}

		Walk();
		Attack(targetObj);

		Death(handlerDeath);
		Delete();
	}


}
